
using MediatR;
using Microsoft.Extensions.Logging;
using Sarafi.Application.Interfaces.Services;
using System.Diagnostics;

namespace Sarafi.Application.Common.Behaviors;

public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly IUserService _userService;

    public PerformanceBehavior(ILogger<TRequest> logger, IUserService userService)
    {
        _timer = new Stopwatch();
        _logger = logger;
        _userService = userService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 5000)
        {
            var requestName = typeof(TRequest).Name;

            var userId = _userService.GetUserId();

            _logger.LogWarning(
                $"Long Running Request: {requestName} ({elapsedMilliseconds} milliseconds) {userId} {request}"
            );
        }

        return response;
    }
}