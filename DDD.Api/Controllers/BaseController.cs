﻿using DDD.Domain;
using DDD.Domain.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DDD.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        public readonly ILogger Logger;
        public readonly IHostingEnvironment HostingEnvironment;

        public BaseController(ILogger logger, IHostingEnvironment env)
        {
            HostingEnvironment = env;
            Logger = logger;
        }

        // read authenticated user data into object
        public ProfileViewModel Profile { get => GetProfile(); }

        public string GetModelStateValidationErrors()
        {
            string message = string.Join("; ", ModelState.Values
                                    .SelectMany(a => a.Errors)
                                    .Select(e => e.ErrorMessage));
            return message;
        }

        public string GetErrorMessage(Exception ex, string customMessage = null)
        {
            Logger?.LogError(ex?.Message, ex);

            if (HostingEnvironment.IsDevelopment())
                return ex.ToReadableString();
            else
                return customMessage ?? "Oops, an error occured while working on your request";

        }

        public IActionResult HandleError(Exception ex, string customMessage = null)
        {
            var rsp = new ApiResponse<bool>();

            // show full error details if in debug environment
            if (HostingEnvironment.IsDevelopment())
                rsp.Message = ex.ToBetterString();
            else
                rsp.Message = customMessage ?? "Oops, an error occured while working on your request";

            Logger?.LogError(ex?.Message, ex);
            return Ok(rsp);
        }

        private ProfileViewModel GetProfile()
        {
            if (!Request.HttpContext.User.Identity.IsAuthenticated)
                return null;

            var claims = User.Identity as ClaimsIdentity;

            var profile = new ProfileViewModel();
            if (claims != null)
            {
                profile.Id = GetObjectValue(claims.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                profile.EmailAddress = GetObjectValue(claims.FindFirst(ClaimTypes.Name)?.Value);
            }

            return profile;
        }

        private string GetObjectValue(string obj)
        {
            if (string.IsNullOrEmpty(obj)) return string.Empty;
            return obj;
        }
    }
}