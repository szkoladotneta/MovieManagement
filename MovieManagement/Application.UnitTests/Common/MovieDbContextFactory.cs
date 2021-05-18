using Microsoft.EntityFrameworkCore;
using Moq;
using MovieManagement.Application.Common.Interfaces;
using MovieManagement.Domain.Entities;
using MovieManagement.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Common
{
    public static class MovieDbContextFactory
    {
        public static Mock<MovieDbContext> Create()
        {
            var dateTime = new DateTime(2000, 1, 1);
            var dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(m => m.Now).Returns(dateTime);

            var currentUserMock = new Mock<ICurrentUserService>();
            currentUserMock.Setup(m => m.Email).Returns("user@user.pl");
            currentUserMock.Setup(m => m.IsAuthenticated).Returns(true);

            var options = new DbContextOptionsBuilder<MovieDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var mock = new Mock<MovieDbContext>(options, dateTimeMock.Object, currentUserMock.Object) { CallBase = true };

            var context = mock.Object;

            context.Database.EnsureCreated();

            var director = new MovieManagement.Domain.Entities.Director() { Id = 2, StatusId = 1, DirectorName = new MovieManagement.Domain.ValueObjects.PersonName() { FirstName = "Kajetan", LastName = "Duszyński" } };
            context.Directors.Add(director);

            var directorBiography = new DirectorBiography() { DirectorId = 2, Id = 2, DoB = new DateTime(1950, 1, 1), PlaceOfBirth = "Warsaw" };
            context.DirectorBiographies.Add(directorBiography);

            var genre = new Genre() { Id = 1, Name = "Comedy" };
            context.Genres.Add(genre);

            var movie = new Movie() { DirectorId = 2, Genres = new List<Genre>() { genre }, Name = "MovieName", PremiereYear = 2000, Id = 3 };

            context.Movies.Add(movie);

            context.SaveChanges();

            return mock;
        }

        public static void Destroy(MovieDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
