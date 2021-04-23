using Microsoft.EntityFrameworkCore;
using MovieManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MovieManagement.Application.Common.Interfaces
{
    public interface IMovieDbContext
    {
        DbSet<Director> Directors { get; set; }
        DbSet<Movie> Movies { get; set; }
        DbSet<DirectorBiography> DirectorBiographies { get; set; }
        DbSet<Genre> Genres { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
