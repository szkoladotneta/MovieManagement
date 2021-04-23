using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieManagement.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MovieManagement.Application.Directors.Queries.GetDirectorDetail
{
    public class GetDirectorDetailQueryHandler : IRequestHandler<GetDirectorDetailQuery, DirectorDetailVm>
    {
        private readonly IMovieDbContext _context;
        private IMapper _mapper;
        public GetDirectorDetailQueryHandler(IMovieDbContext movieDbContext, IMapper mapper)
        {
            _context = movieDbContext;
            _mapper = mapper;
        }
        public async Task<DirectorDetailVm> Handle(GetDirectorDetailQuery request, CancellationToken cancellationToken)
        {
            var director = await _context.Directors.Include(p => p.Movies).Where(p => p.Id == request.DirectorId).FirstOrDefaultAsync(cancellationToken);

            var directorVm = _mapper.Map<DirectorDetailVm>(director);

            return directorVm;
        }
    }
}
