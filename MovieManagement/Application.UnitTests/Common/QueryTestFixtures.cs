using AutoMapper;
using MovieManagement.Application.Common.Mappings;
using MovieManagement.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Common
{
    public class QueryTestFixtures : IDisposable
    {
        public MovieDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public QueryTestFixtures()
        {
            Context = MovieDbContextFactory.Create().Object;

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }
        public void Dispose()
        {
            MovieDbContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixtures> { }
}
