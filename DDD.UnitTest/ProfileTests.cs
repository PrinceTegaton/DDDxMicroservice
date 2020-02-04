using DDD.Core.Managers;
using DDD.Domain.Models;
using DDD.Infrastructure.DataAccess;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.UnitTest
{
    public class ProfileTest
    {
        private ProfileManager _profileManager;

        [SetUp]
        public void BuildContext()
        {
            var connection = new SqliteConnection("Filename=DDD_ProfileTests.db");
            connection.Open();

            var opt1 = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("DDD_Db").Options;
            var opt2 = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connection).Options;

            var context = new AppDbContext(opt2);
            context.Database.EnsureCreated();

            var serviceProvider = new ServiceCollection()
                                        .AddLogging()
                                        .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<UserProfile>();

            _profileManager = new ProfileManager(context, logger);
        }

        [Test]
        public void CreateProfile()
        {
            var profile = _profileManager.CreateProfile(new UserProfile
            {
                Name = "Prince Tegaton",
                EmailAddress = "emailaddress@xmail.com",
                PhoneNumber = "070500000000",
                Country = "NG",
                Location = "Lagos",
                CreatedBy = "admin",
            }).Result;

            Assert.IsNotNull(profile);
        }

        [Test]
        public void GetProfile()
        {
            var result = _profileManager.GetProfile(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void DeleteAllProfiles()
        {
            var profile1 = _profileManager.CreateProfile(new UserProfile
            {
                Name = "Prince Tegaton 1",
                EmailAddress = "emailaddress1@xmail.com",
                PhoneNumber = "070500000000",
                Country = "NG",
                Location = "Lagos"
            });

            var profile2 = _profileManager.CreateProfile(new UserProfile
            {
                Name = "Prince Tegaton 2",
                EmailAddress = "emailaddress2@xmail.com",
                PhoneNumber = "070500000000",
                Country = "NG",
                Location = "Abuja"
            });

            var result = _profileManager.DeleteAllProfiles();

            Assert.IsTrue(result > 0);
        }
    }
}
