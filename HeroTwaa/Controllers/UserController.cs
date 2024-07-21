using AutoMapper;
using HeroTwaa.Helpers;
using HeroTwaa.Models;
using HeroTwaa.Models.DTOs;
using HeroTwaa.Repositories;
using HeroTwaa.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Web;
using static HeroTwaa.Helpers.FileHelper;

namespace HeroTwaa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<UserController> _logger;

        public UserController(ITokenService tokenService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUnitOfWork unitOfWork, IMapper mapper, IEmailSender emailSender, ILogger<UserController> logger)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailSender = emailSender;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _unitOfWork.Users.GetAllUsersAsync();
            var userDTOs = _mapper.Map<IEnumerable<UserDTO>>(users);
            return Ok(userDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(string id)
        {
            var user = await _unitOfWork.Users.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new ErrorResponse { Message = "User not found", Details = new[] { $"No user with ID {id} found" } });
            }
            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDTO model)
        {
            var user = _mapper.Map<ApplicationUser>(model);
            user.UserName = model.Email;
            var generatedPassword = PasswordHelper.GenerateRandomPassword();
            var result = await _userManager.CreateAsync(user, generatedPassword);
            if (result.Succeeded)
            {
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.Users.AddToRoleAsync(user, model.Role.ToString());

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var encodedToken = HttpUtility.UrlEncode(token);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "User", new { token = encodedToken, email = user.Email }, Request.Scheme);

                // Log the token and confirmation link for testing
                _logger.LogInformation($"Confirmation Token: {token}");
                _logger.LogInformation($"Encoded Token: {encodedToken}");
                _logger.LogInformation($"Confirmation Link: {confirmationLink}");

                await _emailSender.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your email by clicking <a href='{confirmationLink}'>here</a>.");

                // Store the generated password in a secure way until the user confirms their email
                user.SecurityStamp = generatedPassword; // Temporary storage of password
                await _userManager.UpdateAsync(user);

                return Ok("Registration successful. Please check your email to confirm your account.");
            }

            return BadRequest(new ErrorResponse { Message = "User registration failed", Details = result.Errors.Select(e => e.Description) });
        }


        [HttpPost("confirmemail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDTO model)
        {
            if (string.IsNullOrWhiteSpace(model.Token) || string.IsNullOrWhiteSpace(model.Email))
            {
                _logger.LogWarning("Token or email is missing.");
                return BadRequest("Token and email are required.");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                _logger.LogWarning($"User not found with email: {model.Email}");
                return BadRequest("Invalid email.");
            }

            var decodedToken = HttpUtility.UrlDecode(model.Token);
            _logger.LogInformation($"Decoded Token: {decodedToken}");

            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            if (!result.Succeeded)
            {
                _logger.LogWarning("Email confirmation failed.");
                return BadRequest("Email confirmation failed.");
            }

            // Retrieve the generated password from the secure storage
            var generatedPassword = user.SecurityStamp;

            // Send the generated password to the user's email
            await _emailSender.SendEmailAsync(user.Email, "Your new password", $"Your account has been confirmed. Here is your new password: {generatedPassword}");

            // Clear the stored password
            user.SecurityStamp = null;
            await _userManager.UpdateAsync(user);

            return Ok("Email confirmed successfully. A new password has been sent to your email.");
        }



        [HttpPost("registerrange")]
        public async Task<ActionResult> RegisterRange(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new ErrorResponse { Message = "File is empty or not provided", Details = new[] { "Please upload a valid file" } });
            }

            List<UserData> usersData = new List<UserData>();
            string fileExtension = Path.GetExtension(file.FileName).ToLower();

            using (var stream = file.OpenReadStream())
            {
                switch (fileExtension)
                {
                    case ".xlsx":
                        usersData = FileHelper.ParseExcelFile(stream);
                        break;
                    case ".pdf":
                        usersData = FileHelper.ParsePdfFile(stream);
                        break;
                    case ".docx":
                        usersData = FileHelper.ParseDocxFile(stream);
                        break;
                    default:
                        return BadRequest(new ErrorResponse { Message = "Unsupported file type", Details = new[] { "Please upload a .xlsx, .pdf, or .docx file" } });
                }
            }

            var users = usersData.Select(data => new ApplicationUser
            {
                UserName = data.Email, // Ensure UserName is set to Email
                Email = data.Email,
                PhoneNumber = data.PhoneNumber,
                Name = data.Name
            }).ToList();

            var errors = new List<string>();

            foreach (var user in users)
            {
                var existingUser = await _unitOfWork.Users.FindAsync(u => u.UserName == user.UserName);
                if (existingUser.Any())
                {
                    errors.Add($"Username '{user.UserName}' is already taken.");
                    continue;
                }

                var result = await _unitOfWork.Users.RegisterUserAsync(user, "DefaultPassword123!"); // Use a default password or generate one
                if (!result.Succeeded)
                {
                    errors.AddRange(result.Errors.Select(e => e.Description));
                    continue;
                }

                // Assign the correct role
                var userData = usersData.First(u => u.Email == user.Email);
                if (Enum.TryParse<HeroTwaa.Models.UserRole>(userData.Role, out HeroTwaa.Models.UserRole userRole))
                {
                    await _unitOfWork.Users.AddToRoleAsync(user, userRole.ToString());
                }
                else
                {
                    errors.Add($"Role {userData.Role} is not valid for user '{user.Email}'.");
                }
            }

            await _unitOfWork.CompleteAsync();

            if (errors.Any())
            {
                return BadRequest(new ErrorResponse { Message = "User registration failed for some users", Details = errors });
            }

            return Ok(new { Message = "User registration completed", Details = "Some users might have been skipped due to already existing accounts." });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            if (!user.EmailConfirmed)
            {
                return Unauthorized(new { message = "Email not confirmed." });
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);
            if (!result.Succeeded)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            var token = await _tokenService.GenerateToken(user);
            return Ok(new { token });
        }

        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _unitOfWork.Users.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            var userDTO = new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
            };

            return Ok(userDTO);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await _unitOfWork.Users.LogoutUserAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, UpdateUserDTO model)
        {
            var user = await _unitOfWork.Users.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new ErrorResponse { Message = "User not found", Details = new[] { $"No user with ID {id} found" } });
            }

            // Update only the properties that are provided in the DTO
            if (!string.IsNullOrEmpty(model.Name))
            {
                user.Name = model.Name;
            }
            if (!string.IsNullOrEmpty(model.Email))
            {
                user.Email = model.Email;
                user.UserName = model.Email; // Ensure username is updated if email is changed
            }
            if (!string.IsNullOrEmpty(model.PhoneNumber))
            {
                user.PhoneNumber = model.PhoneNumber;
            }
            if (model.Role.HasValue)
            {
                var roles = await _unitOfWork.Users.GetRolesAsync(user);
                if (roles.Any())
                {
                    await _unitOfWork.Users.RemoveFromRolesAsync(user, roles);
                }
                await _unitOfWork.Users.AddToRoleAsync(user, model.Role.Value.ToString());
            }

            var result = await _unitOfWork.Users.UpdateUserAsync(user);
            if (result.Succeeded)
            {
                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            return BadRequest(new ErrorResponse { Message = "User update failed", Details = result.Errors.Select(e => e.Description) });
        }
    
    [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _unitOfWork.Users.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new ErrorResponse { Message = "User not found", Details = new[] { $"No user with ID {id} found" } });
            }

            var result = await _unitOfWork.Users.DeleteUserAsync(user);
            if (result.Succeeded)
            {
                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            return BadRequest(new ErrorResponse { Message = "User deletion failed", Details = result.Errors.Select(e => e.Description) });
        }

        [HttpDelete("deleterange")]
        public async Task<IActionResult> DeleteRange([FromBody] List<string> usernames)
        {
            if (usernames == null || !usernames.Any())
            {
                return BadRequest(new ErrorResponse { Message = "No usernames provided", Details = new[] { "Please provide a list of usernames to delete." } });
            }

            var errors = new List<string>();
            var usersToDelete = new List<ApplicationUser>();

            foreach (var username in usernames)
            {
                var user = await _unitOfWork.Users.FindAsync(u => u.UserName == username);
                if (user.Any())
                {
                    usersToDelete.AddRange(user);
                }
                else
                {
                    errors.Add($"User '{username}' not found.");
                }
            }

            if (usersToDelete.Any())
            {
                _unitOfWork.Users.RemoveRange(usersToDelete);
                await _unitOfWork.CompleteAsync();
            }

            if (errors.Any())
            {
                return BadRequest(new ErrorResponse { Message = "Some users could not be deleted", Details = errors });
            }

            return Ok(new { Message = "Users deleted successfully", DeleteUsers = _mapper.Map<IEnumerable<UserDTO>>(usersToDelete) });
        }
    }
}
