using MovieManagement.Domain.Entities;
using MovieManagement.Persistance;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.IntegrationTests.Common
{
    public static class Utilities
    {
        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }
        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }

        public static void InitializeDbForTests(MovieDbContext context)
        {

            var director = new Director() { Id = 2, StatusId = 1, DirectorName = new MovieManagement.Domain.ValueObjects.PersonName { FirstName = "Fake", LastName = "Name" } };

            context.Directors.Add(director);

            var directorBiography = new DirectorBiography() { DirectorId = 2, Id = 2, DoB = new DateTime(1950, 1, 1), PlaceOfBirth = "Warsaw" };
            context.DirectorBiographies.Add(directorBiography);

            var genre = new Genre() { Id = 1, Name = "Comedy" };
            context.Genres.Add(genre);

            var movie = new Movie() { DirectorId = 2, Genres = new List<Genre>() { genre }, Name = "MovieName", PremiereYear = 2000, Id = 3 };

            context.Movies.Add(movie);

            context.SaveChanges();
        }
    }
}
