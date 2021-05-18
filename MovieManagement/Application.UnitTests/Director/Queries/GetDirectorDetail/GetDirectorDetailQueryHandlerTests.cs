using Application.UnitTests.Common;
using AutoMapper;
using MovieManagement.Application.Directors.Queries.GetDirectorDetail;
using MovieManagement.Persistance;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Director.Queries.GetDirectorDetail
{
    [Collection("QueryCollection")]
    public class GetDirectorDetailQueryHandlerTests
    {
        private readonly MovieDbContext _context;
        private readonly IMapper _mapper;

        public GetDirectorDetailQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
            _mapper = fixtures.Mapper;
        }

        [Fact]
        public async Task CanGetDirectorDetailById()
        {
            var handler = new GetDirectorDetailQueryHandler(_context, _mapper);
            var directorId = 2;

            var result = await handler.Handle(new GetDirectorDetailQuery { DirectorId = directorId }, CancellationToken.None);
            result.ShouldBeOfType<DirectorDetailVm>();
            result.FullName.ShouldBe("Kajetan Duszyński");
        }
    }
}
