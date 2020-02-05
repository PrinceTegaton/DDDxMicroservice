using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.Core.Managers;
using DDD.Domain;
using DDD.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DDD.Api.Controllers
{
    public class SystemController : BaseController
    {
        public SystemController(ILogger<SystemController> logger, IHostingEnvironment env) : base(logger, env)
        {
            
        }

        /// <summary>
        /// Check app state on the server
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [HttpGet]
        public IActionResult Ping()
        {
            return Ok(new ApiResponse
            {
                StatusCode = ResponseCodes.Ok,
                Message = $"Hello person. I am alive and well even if you called me @ {DateTime.Now}"
            });
        }
    }
}
