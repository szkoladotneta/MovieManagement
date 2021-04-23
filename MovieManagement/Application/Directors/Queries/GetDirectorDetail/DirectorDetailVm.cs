using AutoMapper;
using MovieManagement.Application.Common.Mappings;
using MovieManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Application.Directors.Queries.GetDirectorDetail
{
    public class DirectorDetailVm : IMapFrom<Director>
    {
        public string FullName { get; set; }
        public string LastMovieName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Director, DirectorDetailVm>()
                .ForMember(d => d.FullName, map => map.MapFrom(src => src.DirectorName.ToString()))
                .ForMember(d => d.LastMovieName, map => map.MapFrom<LastMovieNameResolver>());
        }

        private class LastMovieNameResolver : IValueResolver<Director, object, string>
        {
            public string Resolve(Director source, object destination, string destMember, ResolutionContext context)
            {
                if (source.Movies is not null && source.Movies.Any())
                {
                    var lastMovie = source.Movies.OrderByDescending(p => p.PremiereYear).FirstOrDefault();
                    return lastMovie.Name;
                }
                return string.Empty;
            }
        }
    }
}
