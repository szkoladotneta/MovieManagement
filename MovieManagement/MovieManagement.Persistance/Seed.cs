using Microsoft.EntityFrameworkCore;
using MovieManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Persistance
{
    public static class Seed
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Director>(d =>
            {
                d.HasData(new Director()
                {
                    Id = 1,
                    StatusId = 1,
                    Created = DateTime.Now
                });
                d.OwnsOne(d => d.DirectorName).HasData(new { DirectorId = 1, FirstName = "Andrzej", LastName = "Wajda" });
            }
                );

            modelBuilder.Entity<Movie>().HasData(
                new Movie() { Id = 1, DirectorId = 1, Name = "Pan Tadeusz" },
                new Movie() { Id = 2, DirectorId = 1, Name = "Popiół i Diament" }
                );
        }
    }
}
