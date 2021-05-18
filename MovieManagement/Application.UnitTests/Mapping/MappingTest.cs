using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Mapping
{
    public class MappingTest : IClassFixture<MappingTestFixture>
    {
        private readonly IConfigurationProvider _configration;
        private readonly IMapper _mapper;

        public MappingTest(MappingTestFixture fixture)
        {
            _configration = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configration.AssertConfigurationIsValid();
        }
    }
}
