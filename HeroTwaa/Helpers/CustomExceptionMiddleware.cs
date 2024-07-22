using HeroTwaa.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;

namespace HeroTwaa.Helpers
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
        {
            _next = next;   
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception) 
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new CustomErrorResponse
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = "Internal Server Error. An unexpected error occurred.",
                Details = new[] { exception.Message }
            };

            return httpContext.Response.WriteAsync(response.ToString());
        }
    }
}
