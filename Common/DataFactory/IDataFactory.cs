using Common.Messaging;

namespace Common.DataFactory;

public interface IDataFactory<TMessage, TMetadata, TUnverified, out TVerified>
    where TMessage : Message
    where TMetadata : MessageMetadata
{
    Task<TUnverified> GetDataAsync(
        MessageContainer<TMessage, TMetadata> container);

    TVerified GetVerifiedData(TUnverified unverified);
}