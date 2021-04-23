using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.Application.Directors.Commands.CreateDirector;
using MovieManagement.Application.Directors.Queries.GetDirectorDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagement.Api.Controllers
{
    [Route("api/directors")]
    public class DirectorsController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<DirectorDetailVm>> GetDetails(int id)
        {
            var vm = await Mediator.Send(new GetDirectorDetailQuery() { DirectorId = id });
            return vm;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDirector(CreateDirectorCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
