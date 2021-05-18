using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieManagement.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MovieManagement.Application.Directors.Queries.GetDirectors
{
    public class GetDirectorsQueryHandler : IRequestHandler<GetDirectorsQuery, DirectorsVm>
    {
        private readonly IMovieDbContext _context;
        private IMapper _mapper;
        public GetDirectorsQueryHandler(IMovieDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DirectorsVm> Handle(GetDirectorsQuery getDirectorsQuery, CancellationToken none)
        {
            var directors = await _context.Directors.AsNoTracking().ProjectTo<DirectorDto>(_mapper.ConfigurationProvider).ToListAsync();

            return new DirectorsVm() { Directors = directors };
        }
    }
}
