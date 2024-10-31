using Common.Messaging;

namespace HandlerTemplate.Events.RemoveCommand;

public record RemoveCommandAuthorizationFailedEvent(string Reason) : Message;