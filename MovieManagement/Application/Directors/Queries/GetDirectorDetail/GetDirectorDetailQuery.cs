using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Application.Directors.Queries.GetDirectorDetail
{
    public class GetDirectorDetailQuery : IRequest<DirectorDetailVm>
    {
        public int DirectorId { get; set; }
    }
}
