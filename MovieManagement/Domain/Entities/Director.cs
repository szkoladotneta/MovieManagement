using MovieManagement.Domain.Common;
using MovieManagement.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Domain.Entities
{
    public class Director : AuditableEntity
    {
        public PersonName DirectorName { get; set; }
        public ICollection<Movie> Movies { get; set; }
        public DirectorBiography DirectorBiography { get; set; }

    }
}
