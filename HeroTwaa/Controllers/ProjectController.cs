using AutoMapper;
using HeroTwaa.Models;
using HeroTwaa.Models.ProjectDTOs;
using HeroTwaa.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeroTwaa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProjectController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjects()
        {
            try
            {
                var projects = await _unitOfWork.Projects.GetAllProjectsAsync();
                var projectDTOs = _mapper.Map<IEnumerable<ProjectDTO>>(projects);
                return Ok(projectDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get projects: {ex}");
                throw; 
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> GetProject(int id)
        {
            try
            {
                var project = await _unitOfWork.Projects.GetProjectByIdAsync(id);
                if (project == null)
                {
                    return NotFound(new CustomErrorResponse
                    {
                        StatusCode = 404,
                        Message = "Project not found",
                        Details = new[] { $"No project with ID {id} was found." }
                    });
                }
                var projectDTO = _mapper.Map<ProjectDTO>(project);
                return Ok(projectDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get project by id {id}: {ex}");
                throw; 
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> CreateProject([FromBody] CreateProjectDTO model)
        {
            try
            {
                var project = _mapper.Map<Project>(model);
                _unitOfWork.Projects.Add(project);
                await _unitOfWork.CompleteAsync();

                var projectDTO = _mapper.Map<ProjectDTO>(project);
                return CreatedAtAction(nameof(GetProject), new { id = project.Id }, projectDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create project: {ex}");
                throw; // This will be caught by the middleware
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectDTO model)
        {
            try
            {
                var project = await _unitOfWork.Projects.GetProjectByIdAsync(id);
                if (project == null)
                {
                    return NotFound(new CustomErrorResponse
                    {
                        StatusCode = 404,
                        Message = "Project not found",
                        Details = new[] { $"No project with ID {id} was found." }
                    });
                }

                _mapper.Map(model, project);
                _unitOfWork.Projects.Update(project);
                await _unitOfWork.CompleteAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to update project with id {id}: {ex}");
                throw; 
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                var project = await _unitOfWork.Projects.GetProjectByIdAsync(id);
                if (project == null)
                {
                    return NotFound(new CustomErrorResponse
                    {
                        StatusCode = 404,
                        Message = "Project not found",
                        Details = new[] { $"No project with ID {id} was found." }
                    });
                }

                _unitOfWork.Projects.Remove(project);
                await _unitOfWork.CompleteAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete project with id {id}: {ex}");
                throw;
            }
        }
    }
}
