using ErrorOr;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.Common.Behaviors;
    /// <summary>
    /// Реализация поведения валидации в пайплайне обработки запросов с использованием паттерна.
    /// </summary>
public class ValidationBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;
    public ValidationBehavior(IValidator<TRequest>? validator=null)
    {
        _validator = validator;
    }

    /// <summary>
    /// Реализация обработки запроса,проверка наличия валидатора и обрабатывает ошибки валидации.
    /// </summary>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is null)
        {
            return await next();
        }
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        var errors = validationResult.Errors.ConvertAll(validationFailure => Error.
        Validation(validationFailure.PropertyName, validationFailure.ErrorMessage));
        if (errors.Count == 0)
        {
            return await next();
        }
        return (dynamic) errors;
    }
}
