using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MovieManagement.Application.Common.Behaviours
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger;
        private readonly Stopwatch _timer;
        public PerformanceBehaviour(ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsed = _timer.ElapsedMilliseconds;

            if (elapsed > 500)
            {
                var requestName = typeof(TRequest).Name;

                _logger.LogInformation("MovieManagement Long Running Request: {Name} ({elapsed} milliseconds) {@Request}",
                    requestName,elapsed, request);
            }

            return response;
        }
    }
}
