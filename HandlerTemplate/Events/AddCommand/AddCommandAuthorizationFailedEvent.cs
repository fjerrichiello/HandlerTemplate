using Common.Messaging;

namespace HandlerTemplate.Events.AddCommand;

public record AddCommandAuthorizationFailedEvent(string Reason) : Message;