namespace Common.Messaging;

public abstract record FailureMessage(string Reason) : Message;