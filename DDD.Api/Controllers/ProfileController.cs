using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.Core.Managers;
using DDD.Domain;
using DDD.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DDD.Api.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly IProfileManager _profileManager;

        public ProfileController(ILogger<UserProfile> logger, IHostingEnvironment env, IProfileManager profileManager) : base(logger, env)
        {
            _profileManager = profileManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            try
            {
                var profiles = await _profileManager.GetAllProfiles();

                var rsp = new ApiResponse<IEnumerable<UserProfile>>
                {
                    StatusCode = ResponseCodes.Ok,
                    Message = "User profiles retrieved ok",
                    Data = profiles
                };

                return Ok(rsp);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
                //return HandleError(ex, "An error occured while retrieving profiles");
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            return  Ok(await _profileManager.GetProfile(id));
        }

        [HttpGet]
        public IActionResult ShowError()
        {
            throw new Exception("This exception is handled globally without a TRY...CATCH... block");
        }
    }
}
