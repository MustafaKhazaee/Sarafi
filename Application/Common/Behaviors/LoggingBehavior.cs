using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Sarafi.Application.Interfaces.Services;

namespace Sarafi.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IRequestPreProcessor<TRequest>
{
    private readonly ILogger<TRequest> _logger;
    private readonly IUserService _userService;

    public LoggingBehavior(ILogger<TRequest> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        var userId = _userService.GetUserId();

        string userName = _userService.GetUserName();

        _logger.LogInformation(
            $"Request Name : {requestName} | UserId: {userId} | UserName: {userName} | Request: {request}"
        );
    }
}