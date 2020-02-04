using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using DDD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DDD.Infrastructure.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        // database entities
        public DbSet<UserProfile> Profile { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // ignore deleted records on all 'select' queries
            modelBuilder.Entity<UserProfile>().HasQueryFilter(a => !a.IsDeleted);
        }
    }
}
