using Common.Messaging;

namespace HandlerTemplate.Commands;

public record RemoveCommand(int? Value1) : Message;