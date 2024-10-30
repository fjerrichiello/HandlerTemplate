using Common.Messaging;

namespace HandlerTemplate.Events;

public record FailedEvent(string Reason) : Message;