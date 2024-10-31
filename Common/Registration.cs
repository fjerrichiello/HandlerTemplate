using Common.Authorization;
using Common.DataFactory;
using Common.Messaging;
using Common.Processors;
using Common.Utils;
using Common.Validation;
using Dumpify;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Common;

public static class Registration
{
    private static readonly Type GenericMessageContainerHandlerType = typeof(IMessageContainerHandler<,>);
    private static readonly Type GenericDataQueryType = typeof(IDataFactory<,,,>);
    private static readonly Type GenericAuthorizerType = typeof(IAuthorizer<,,,>);
    private static readonly Type GenericMessageValidatorType = typeof(IMessageValidator<,,,>);
    private static readonly Type GenericProcessorType = typeof(IProcessor<,,,>);

    public static IServiceCollection AddEventHandlersAndNecessaryWork(this IServiceCollection services,
        params Type[] sourceTypes)
    {
        ValidatorOptions.Global.DisplayNameResolver = (type, member, expression) => member?.Name.ToSnakeCase();

        var registrations = sourceTypes.Select(sourceType => sourceType.Assembly).Distinct()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(IsAllowedType)
            .SelectMany(usageType => usageType.GetInterfaces().Where(IsAllowedInterfaceType).Select(x => new
            {
                UsageType = usageType,
                UsageInterfaceType = x
            })).ToList();

        registrations.Dump();

        foreach (var registration in registrations)
        {
            services.AddScoped(registration.UsageInterfaceType, registration.UsageType);
        }

        services.AddScoped<IEventPublisher, EventPublisher>();

        return services;
    }


    private static bool IsAllowedType(Type type)
        => IsMessageContainerHandler(type) ||
           IsDataFactory(type) ||
           IsAuthorizer(type) ||
           IsMessageValidator(type) ||
           IsProcessor(type);

    private static bool IsAllowedInterfaceType(Type type)
        => IsMessageContainerHandlerInterface(type) ||
           IsDataFactoryInterface(type) ||
           IsAuthorizerInterface(type) ||
           IsMessageValidatorInterface(type) ||
           IsProcessorInterface(type);

    private static bool IsMessageContainerHandler(Type type)
    {
        return !type.IsAbstract && type.GetInterfaces().Any(IsMessageContainerHandlerInterface);
    }

    private static bool IsMessageContainerHandlerInterface(Type interfaceType)
    {
        return interfaceType.IsGenericType &&
               interfaceType.GetGenericTypeDefinition() == GenericMessageContainerHandlerType;
    }


    private static bool IsDataFactory(Type type)
    {
        return !type.IsAbstract && type.GetInterfaces().Any(IsDataFactoryInterface);
    }

    private static bool IsDataFactoryInterface(Type interfaceType)
    {
        return interfaceType.IsGenericType &&
               interfaceType.GetGenericTypeDefinition() == GenericDataQueryType;
    }

    private static bool IsAuthorizer(Type type)
    {
        return !type.IsAbstract && type.GetInterfaces().Any(IsAuthorizerInterface);
    }

    private static bool IsAuthorizerInterface(Type interfaceType)
    {
        return interfaceType.IsGenericType &&
               interfaceType.GetGenericTypeDefinition() == GenericAuthorizerType;
    }


    private static bool IsMessageValidator(Type type)
    {
        return !type.IsAbstract && type.GetInterfaces().Any(IsMessageValidatorInterface);
    }

    private static bool IsMessageValidatorInterface(Type interfaceType)
    {
        return interfaceType.IsGenericType &&
               interfaceType.GetGenericTypeDefinition() == GenericMessageValidatorType;
    }

    private static bool IsProcessor(Type type)
    {
        return !type.IsAbstract && type.GetInterfaces().Any(IsProcessorInterface);
    }

    private static bool IsProcessorInterface(Type interfaceType)
    {
        return interfaceType.IsGenericType &&
               interfaceType.GetGenericTypeDefinition() == GenericProcessorType;
    }
}