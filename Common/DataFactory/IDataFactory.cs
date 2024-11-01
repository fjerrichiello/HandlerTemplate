using Common.Messaging;

namespace Common.DataFactory;

public interface IDataFactory<TMessage, TMessageMetadata, TUnverified, out TVerified>
    where TMessage : Message
    where TMessageMetadata : MessageMetadata
{
    Task<TUnverified> GetDataAsync(
        MessageContainer<TMessage, TMessageMetadata> container);

    TVerified GetVerifiedData(TUnverified unverified);
}