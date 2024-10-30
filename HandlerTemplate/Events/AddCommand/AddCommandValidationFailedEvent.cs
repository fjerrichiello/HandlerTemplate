using Common.Messaging;

namespace HandlerTemplate.Events.AddCommand;

public record AddCommandValidationFailedEvent(string Reason) : Message;