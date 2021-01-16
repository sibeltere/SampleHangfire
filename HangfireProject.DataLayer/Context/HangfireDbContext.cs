using HangfireProject.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HangfireProject.DataLayer.Context
{
    public class HangfireDbContext : DbContext
    {
        public HangfireDbContext()
        {
        }

        public HangfireDbContext(DbContextOptions<HangfireDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DT636;Database=ProjectDb;Trusted_Connection=true");
        }

        public DbSet<User> User { get; set; }
      
    }
}
