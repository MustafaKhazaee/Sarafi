﻿
using FluentValidation;
using MediatR;
using Sarafi.Application.Common.Exceptions;

namespace Sarafi.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                failures.ForEach(f => throw new SarafiException(
                    $"Error Message: {f.ErrorMessage}\nError Code: {f.ErrorMessage}\nProperty Name: {f.PropertyName}\nAttempted Value: {f.AttemptedValue}\n"
                ));
            }
            return await next();
        }
    }
}