namespace Common.Messaging;

public abstract record FailedMessage : Message
{
    public string Reason { get; set; }
};