using Common.Messaging;

namespace HandlerTemplate.Commands;

public record AddCommand(int? Value1) : Message;