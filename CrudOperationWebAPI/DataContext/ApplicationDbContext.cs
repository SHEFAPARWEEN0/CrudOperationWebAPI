using CrudOperationWebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CrudOperationWebAPI.DataContext
{
    
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }
            public DbSet<UserDetails> UserDetails { get; set; }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                //  base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<UserDetails>().HasKey(s => s.Id);
            }
        }
    
}
