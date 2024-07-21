using AutoMapper.Internal;
using HeroTwaa.Models;

namespace HeroTwaa.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
