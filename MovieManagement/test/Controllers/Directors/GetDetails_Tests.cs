using MovieManagement.Api;
using MovieManagement.Application.Directors.Queries.GetDirectorDetail;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Directors
{
    public class GetDetails_Tests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public GetDetails_Tests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenDirectorId_ReturnsDirectorsDetails()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            string id = "1";
            var response = await client.GetAsync($"/api/directors/{id}");
            response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<DirectorDetailVm>(response);

            vm.ShouldNotBeNull();
        }
    }
}
