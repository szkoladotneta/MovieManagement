using MovieManagement.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.IntegrationTests.Common.DummyServices
{
    public class DummyCurrentUserService : ICurrentUserService
    {
        public string Email { get; set; } = "user@user.pl";
        public bool IsAuthenticated { get; set; } = true;
    }
}
