﻿
using MediatR;
using Microsoft.Extensions.Logging;
using Sarafi.Application.Common.Exceptions;

namespace Sarafi.Application.Common.Behaviors
{
    public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        public UnhandledExceptionBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (SarafiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;

                _logger.LogError(ex,
                    $"Error Message: {ex.Message} | Request: ${request} | Request Name: ${requestName} | Stack Trace: ${ex.StackTrace}"
                );

                throw;
            }
        }
    }
}