using Common.Authorization;
using Common.DataQuery;
using Common.Mappers;
using Common.Messaging;
using Common.Processors;
using Common.Utils;
using Common.Validation;
using Common.Verification;
using Dumpify;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Common;

public static class Registration
{
    private static readonly Type GenericMessageContainerHandlerType = typeof(IMessageContainerHandler<,>);
    private static readonly Type GenericDataQueryType = typeof(IDataQuery<,,>);
    private static readonly Type GenericVerifierType = typeof(IVerifier<,,,>);
    private static readonly Type GenericAuthorizerType = typeof(IAuthorizer<,>);
    private static readonly Type GenericValidatorType = typeof(IInternalValidator<,>);
    private static readonly Type GenericMapperType = typeof(IMapper<,>);
    private static readonly Type GenericProcessorType = typeof(IProcessor<,,>);

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
           IsDataQuery(type) ||
           IsVerifier(type) ||
           IsAuthorizer(type) ||
           IsValidator(type) ||
           IsMapper(type) ||
           IsProcessor(type);

    private static bool IsAllowedInterfaceType(Type type)
        => IsMessageContainerHandlerInterface(type) ||
           IsDataQueryInterface(type) ||
           IsVerifierInterface(type) ||
           IsAuthorizerInterface(type) ||
           IsValidatorInterface(type) ||
           IsMapperInterface(type) ||
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


    private static bool IsDataQuery(Type type)
    {
        return !type.IsAbstract && type.GetInterfaces().Any(IsDataQueryInterface);
    }

    private static bool IsDataQueryInterface(Type interfaceType)
    {
        return interfaceType.IsGenericType &&
               interfaceType.GetGenericTypeDefinition() == GenericDataQueryType;
    }

    private static bool IsVerifier(Type type)
    {
        return !type.IsAbstract && type.GetInterfaces().Any(IsVerifierInterface);
    }

    private static bool IsVerifierInterface(Type interfaceType)
    {
        return interfaceType.IsGenericType &&
               interfaceType.GetGenericTypeDefinition() == GenericVerifierType;
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


    private static bool IsValidator(Type type)
    {
        return !type.IsAbstract && type.GetInterfaces().Any(IsValidatorInterface);
    }

    private static bool IsValidatorInterface(Type interfaceType)
    {
        return interfaceType.IsGenericType &&
               interfaceType.GetGenericTypeDefinition() == GenericValidatorType;
    }

    private static bool IsMapper(Type type)
    {
        return !type.IsAbstract && type.GetInterfaces().Any(IsMapperInterface);
    }

    private static bool IsMapperInterface(Type interfaceType)
    {
        return interfaceType.IsGenericType &&
               interfaceType.GetGenericTypeDefinition() == GenericMapperType;
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