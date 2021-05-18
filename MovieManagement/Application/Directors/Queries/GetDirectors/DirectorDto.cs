using AutoMapper;
using MovieManagement.Application.Common.Mappings;
using MovieManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Application.Directors.Queries.GetDirectors
{
    public class DirectorDto : IMapFrom<Director>
    {
        public string FullName { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Director, DirectorDto>()
                .ForMember(d => d.FullName, map => map.MapFrom(src => src.DirectorName.ToString()));
        }
    }
}
