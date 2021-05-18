using Application.UnitTests.Common;
using AutoMapper;
using MovieManagement.Application.Directors.Queries.GetDirectors;
using MovieManagement.Persistance;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Director.Queries.GetDirectors
{
    [Collection("QueryCollection")]
    public class GetDirectorsQueryHandlerTests
    {
        private readonly MovieDbContext _context;
        private readonly IMapper _mapper;

        public GetDirectorsQueryHandlerTests(QueryTestFixtures fixtures)
        {
            _context = fixtures.Context;
            _mapper = fixtures.Mapper;
        }

        [Fact]
        public async Task CanGetDirectors()
        {
            var handler = new GetDirectorsQueryHandler(_context, _mapper);

            var result = await handler.Handle(new GetDirectorsQuery(), CancellationToken.None);

            result.ShouldBeOfType<DirectorsVm>();
            result.Directors.Count.ShouldBe(2);
        }
    }
}
