using Common.Messaging;

namespace HandlerTemplate.Events.AddCommand;

public record AddCommandFailedEvent(string Reason) : Message;