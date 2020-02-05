using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DDD.Api.Authentication
{
    public class APIKeyStoreHandler : AuthorizationHandler<APIKeyStore>
    {
        private readonly IHttpContextAccessor _httpContext;

        public APIKeyStoreHandler(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, APIKeyStore requirement)
        {
            SucceedRequirementIfApiKeyPresentAndValid(context, requirement);
            return Task.CompletedTask;
        }

        private void SucceedRequirementIfApiKeyPresentAndValid(AuthorizationHandlerContext context, APIKeyStore requirement)
        {
            if (context.Resource is AuthorizationFilterContext authorizationFilterContext)
            {
                var apiKey = authorizationFilterContext.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

                if (string.IsNullOrEmpty(apiKey))
                {
                    var rsp = new { message = "Access Denied: No Client_Id supplied" };
                    _httpContext.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                    _httpContext.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    _httpContext.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(rsp));

                    context.Fail();
                    return;
                }

                var client = requirement.Keys.FirstOrDefault(a => a.Key == apiKey);
                if (client == null)
                {
                    var rsp = new { message = "Invalid Client_Id" };
                    _httpContext.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                    _httpContext.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    _httpContext.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(rsp));

                    context.Fail();
                    return;
                };

                if (!client.IsActive)
                {
                    var rsp = new { message = "Client_Id is not active" };
                    _httpContext.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                    _httpContext.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    _httpContext.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(rsp));

                    context.Fail();
                    return;
                }

                var newClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.PrimarySid, client.Key),
                        new Claim(ClaimTypes.NameIdentifier, client.Name)
                    };
                
                context.User.AddIdentity(new ClaimsIdentity(newClaims));

                context.Succeed(requirement);
            }
        }
    }
}
