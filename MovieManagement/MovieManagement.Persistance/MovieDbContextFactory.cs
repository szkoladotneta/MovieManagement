using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Persistance
{
    public class MovieDbContextFactory : DesignTimeDbContextFactoryBase<MovieDbContext>
    {
        protected override MovieDbContext CreateNewInstance(DbContextOptions<MovieDbContext> options)
        {
            return new MovieDbContext(options);
        }
    }
}
