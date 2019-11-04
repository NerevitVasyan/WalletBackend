using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpendingsWebAPI.Entities;
using SpendingsWebAPI.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpendingsWebAPI
{
    public class WalletContext : IdentityDbContext<User>
    {
        public WalletContext(DbContextOptions<WalletContext> options) : base(options)
        {
            Database.EnsureCreated();
            //User u = new User();
            //u.TestExtention();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=(localdb)\mssqllocaldb;Database=Blogging;Trusted_Connection=True;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SpendingTag>().HasKey(x => new { x.SpendingId, x.TagId });

            modelBuilder.Entity<SpendingTag>()
                .HasOne(st => st.Spending)
                .WithMany(s => s.Tags)
                .HasForeignKey(st => st.SpendingId);

            modelBuilder.Entity<SpendingTag>()
                .HasOne(st => st.Tag)
                .WithMany(t => t.Spendings)
                .HasForeignKey(st => st.TagId);

            //ATTENTION
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Spending> Spendings { get; set; }
    }
}
