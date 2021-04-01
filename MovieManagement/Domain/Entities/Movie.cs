using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PremiereYear { get; set; }
        public int DirectorId { get; set; }
        public Director Director { get; set; }
        public ICollection<Genre> Genres { get; set; }
    }
}
