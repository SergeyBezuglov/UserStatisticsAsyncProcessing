using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PIMS.Application.Common.Behaviors;
using System.Reflection;

namespace PIMS.Application;

/// <summary>
/// Внедрение зависимостей
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        ///<summary>
        /// Добавляет MediatR для обработки команд и запросов, регистрация сервисов и обработчиков из DI.
        /// </summary>

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly); 
        
        });
        ///<summary>
        /// Добавляет пайплайн-поведение для валидации запросов и команд.
        /// </summary>
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        ///<summary>
        /// Добавляет все валидаторы из сборки, содержащая данный метод.
        /// </summary>
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        ///<returns>
        /// Возвращает обновленный IServiceCollection.
        /// </returns>
        return services;
    }
}

 