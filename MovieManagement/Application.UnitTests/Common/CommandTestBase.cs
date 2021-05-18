using Moq;
using MovieManagement.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly MovieDbContext _context;
        protected readonly Mock<MovieDbContext> _contextMock;
        public CommandTestBase()
        {
            _contextMock = MovieDbContextFactory.Create();
            _context = _contextMock.Object;
        }
        public void Dispose()
        {
            MovieDbContextFactory.Destroy(_context);
        }
    }
}
