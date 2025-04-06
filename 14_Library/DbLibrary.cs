using _14_Library.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace _14_Library
{
    public class DbLibrary : DbContext
    {
        public DbLibrary()
        {
            //this.Database.EnsureDeleted();

            //this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-JELVTGO\SQLEXPRESS;
                                        Initial Catalog = Library22;
                                        Integrated Security=True;
                                        Connect Timeout=2;Encrypt=False;
                                        Trust Server Certificate=True;
                                        Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>()
                .HasOne(c => c.Author)
                .WithMany(w => w.books)
                .HasForeignKey(c => c.AuthorId);
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

    }
}
