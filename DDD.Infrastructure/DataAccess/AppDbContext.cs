using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using DDD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Infrastructure.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<UserProfile> Profile { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
