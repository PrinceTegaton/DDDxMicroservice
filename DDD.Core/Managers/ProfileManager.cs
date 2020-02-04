using DDD.Domain.Models;
using DDD.Infrastructure.DataAccess;
using DDD.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Core.Managers
{
    public class ProfileManager : ManagerBase<UserProfile>, IProfileManager
    {
        public ProfileManager(DbContext context, ILogger<UserProfile> logger) : base(context, logger)
        {
            
        }

        public async Task<UserProfile> GetProfile(int id)
        {
            this.Logger.LogInformation($"Retrieving user profile, id: {id}");
            return await this.GetByIdAsync(id);
        }

        public int DeleteAllProfiles()
        {
            var all = this.GetAll();
            int count = 0;

            foreach (var i in all)
            {
                this.Remove(i);
                count++;
            }
            
            return count;
        }

        public async Task<UserProfile> CreateProfile(UserProfile profile)
        {
            this.Logger.LogInformation("Creating user profile --> {@profile}", profile);

            bool r = await this.AddAsync(profile);            
            return r ? profile : null;
        }
    }
}
