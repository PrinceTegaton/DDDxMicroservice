﻿using System;
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

        public ProfileController(
            ILogger<UserProfile> logger, 
            IHostingEnvironment env, 
            IProfileManager profileManager) : base(logger, env)
        {
            _profileManager = profileManager;
        }

        /// <summary>
        /// Retrieve all saved user profiles
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<UserProfile>>), 200)]
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

        /// <summary>
        /// Get user profile by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiResponse<UserProfile>), 200)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            return  Ok(await _profileManager.GetProfile(id));
        }

        /// <summary>
        /// Test method
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiResponse<DateTime>), 200)]
        [HttpGet]
        public IActionResult Test()
        {
            return Ok(new ApiResponse<DateTime>
            {
                StatusCode = ResponseCodes.Ok,
                Message = "Test method 2 days ahead",
                Data = DateTime.Now.AddDays(2)
            });
        }

        /// <summary>
        /// Test global error handling
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [HttpGet]
        public IActionResult ShowError()
        {
            throw new Exception("This exception is handled globally without a TRY...CATCH... block");
        }
    }
}
