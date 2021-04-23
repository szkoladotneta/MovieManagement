using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Application.Directors.Commands.DeleteDirector
{
    public class DeleteDirectorCommand : IRequest
    {
        public int DirectorId { get; set; }
    }
}
