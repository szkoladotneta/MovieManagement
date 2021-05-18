using Application.UnitTests.Common;
using Microsoft.EntityFrameworkCore;
using MovieManagement.Application.Directors.Commands.CreateDirector;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Director.Commands.CreateDirector
{
    public class CreateDirectorCommandHandlerTests : CommandTestBase
    {
        private readonly CreateDirectorCommandHandler _handler;
        public CreateDirectorCommandHandlerTests()
            : base()
        {
            _handler = new CreateDirectorCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldInsertDirector()
        {
            var command = new CreateDirectorCommand()
            {
                FirstName = "Fake",
                LastName = "Name",
                DoB = new DateTime(1999, 1, 1),
                PlaceOfBirth = "Warsaw"
            };

            var result = await _handler.Handle(command, CancellationToken.None);

            var dir = await _context.Directors.FirstAsync(x => x.Id == result, CancellationToken.None);

            dir.ShouldNotBeNull();

        }

    }
}
