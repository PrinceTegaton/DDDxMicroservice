using DDD.Api.Controllers;
using DDD.Domain;
using DDD.Domain.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DDD.Api.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostingEnvironment env)
        {
            _logger = logger;
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // fine-tune log data to taste
                _logger.LogError(ex, $"Exception occured: {ex.ToBetterString()}", null);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string msg = _env.IsDevelopment() ? exception.Message : "Internal Server Error from the custom middleware.";
            var res = JsonConvert.SerializeObject(new ApiResponse
            {
                StatusCode = ResponseCodes.Error,
                Message = msg
            });

            return context.Response.WriteAsync(res);
        }
    }
}
