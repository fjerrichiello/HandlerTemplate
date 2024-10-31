using Common.Messaging;

namespace HandlerTemplate.Events.RemoveCommand;

public record RemoveCommandValidationFailedEvent(string Reason) : Message;