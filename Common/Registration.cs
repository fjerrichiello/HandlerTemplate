using System.Reflection;
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

    private static readonly Type GenericConventionalMessageContainerHandlerType =
        typeof(ConventionalCommandContainerHandler<,,,,,,>);

    private static readonly Type GenericDataQueryType = typeof(IDataFactory<,,,>);
    private static readonly Type GenericAuthorizerType = typeof(IAuthorizer<,,,>);
    private static readonly Type GenericMessageValidatorType = typeof(IMessageValidator<,,,>);
    private static readonly Type GenericProcessorType = typeof(IProcessor<,,,,>);

    public static IServiceCollection AddEventHandlersAndNecessaryWork(this IServiceCollection services,
        params Type[] sourceTypes)
    {
        ValidatorOptions.Global.DisplayNameResolver = (type, member, expression) => member?.Name.ToSnakeCase();

        var registrations = sourceTypes.Select(sourceType => sourceType.Assembly).Distinct()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(IsAllowedType)
            .SelectMany(usageType => usageType.GetInterfaces().Where(IsAllowedInterfaceType).Select(x => new
            {
                MessageType = x.GetGenericArguments().First(),
                MessageMetadataType = x.GetGenericArguments().ElementAt(1),
                UsageType = usageType,
                UsageInterfaceType = x
            }))
            .GroupBy(x => x.MessageType)
            .ToDictionary(group => group.Key, group => group.ToList());

        registrations.Dump();

        foreach (var command in registrations.Keys)
        {
            var handlerDependencies = registrations[command];

            var genericParameters = new HandlerParameters();

            foreach (var handlerDependency in handlerDependencies)
            {
                services.AddScoped(handlerDependency.UsageInterfaceType, handlerDependency.UsageType);

                genericParameters.SetType(handlerDependency.UsageInterfaceType);
            }

            services.AddKeyedScoped(genericParameters.GetGenericInterfaceType(), genericParameters.GetMessageName(),
                genericParameters.GetGenericType());
        }

        services.AddScoped<IEventPublisher, EventPublisher>();

        return services;
    }

    private record HandlerParameters
    {
        public string GetMessageName() => MessageType.Name;

        public Type GetGenericInterfaceType()
            => GenericMessageContainerHandlerType.MakeGenericType(MessageType,
                MessageMetadataType);

        public Type GetGenericType()
            => GenericConventionalMessageContainerHandlerType.MakeGenericType(
                MessageType,
                UnverifiedDataType,
                VerifiedDataType,
                AuthorizationFailedEventType,
                ValidationFailedEventType,
                FailedEventType,
                SuccessEventType);

        private Type MessageType { get; set; }

        private Type MessageMetadataType { get; set; }

        private Type UnverifiedDataType { get; set; }

        private Type VerifiedDataType { get; set; }

        private Type AuthorizationFailedEventType { get; set; }

        private Type ValidationFailedEventType { get; set; }

        private Type FailedEventType { get; set; }

        private Type SuccessEventType { get; set; }

        public void SetType(Type type)
        {
            var genericArguments = type.GetGenericArguments();

            if (IsDataFactoryInterface(type))
            {
                UnverifiedDataType = genericArguments[^2];
                VerifiedDataType = genericArguments.Last();
            }

            if (IsAuthorizerInterface(type))
            {
                AuthorizationFailedEventType = genericArguments.Last();
            }

            if (IsMessageValidatorInterface(type))
            {
                ValidationFailedEventType = genericArguments.Last();
            }

            if (IsProcessorInterface(type))
            {
                MessageType = genericArguments.First();
                MessageMetadataType = genericArguments.Skip(1).First();
                FailedEventType = genericArguments[^2];
                SuccessEventType = genericArguments.Last();
            }
        }
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