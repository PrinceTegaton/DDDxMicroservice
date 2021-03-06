﻿using DDD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Core.Managers
{
    public interface IProfileManager
    {
        Task<IEnumerable<UserProfile>> GetAllProfilesFromStoredProcedure(string searchKeyword);
        Task<IEnumerable<UserProfile>> GetAllProfiles();
        Task<UserProfile> GetProfile(int id);
        int DeleteAllProfiles();
        Task<UserProfile> CreateProfile(UserProfile profile);
    }
}
