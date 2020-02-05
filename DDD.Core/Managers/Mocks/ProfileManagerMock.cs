using System.Collections.Generic;
using System.Threading.Tasks;
using DDD.Domain.Models;
using DDD.Infrastructure.DataAccess;
using DDD.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace DDD.Core.Managers.Mocks
{
    public class ProfileManagerMock : IProfileManager
    {
        private readonly AppDbContext _context;
        private readonly ISimpleRepository<UserProfile> _repo;

        public ProfileManagerMock()
        {
            var opt = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("DDD_Db").Options;
            _context = new AppDbContext(opt);
            _repo = new SimpleRepository<UserProfile>(_context);
        }

        public int DeleteAllProfiles()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<UserProfile>> GetAllProfiles()
        {
            throw new System.NotImplementedException();
        }

        public Task<UserProfile> GetProfile(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserProfile> CreateProfile(UserProfile profile)
        {
            await _context.Profile.AddAsync(profile);
            return profile;
        }

        public Task<IEnumerable<UserProfile>> GetAllProfilesFromStoredProcedure(string searchKeyword)
        {
            throw new System.NotImplementedException();
        }
    }
}
