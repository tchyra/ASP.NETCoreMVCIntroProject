using IntroProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroProject.Services
{
    public class IntroDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Club> Clubs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Membership composite primary key
            builder.Entity<Membership>()
                .HasKey(m => new { m.ClubId, m.PersonId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);

            // Visit https://connectionstrings.com/ :)
            options.UseSqlite("Data Source=intro.db;");
        }
    }
}
