using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Application.Directors.Queries.GetDirectors
{
    public class DirectorsVm
    {
        ICollection<DirectorDto> Directors { get; set; }
    }
}
