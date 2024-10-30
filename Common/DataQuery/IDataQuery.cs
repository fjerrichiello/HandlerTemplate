using Common.Messaging;

namespace Common.DataQuery;

public interface IDataQuery<TMessage, TMetadata, TUnverified>
    where TMessage : Message
    where TMetadata : MessageMetadata
{
    Task<TUnverified> GetDataAsync(
        MessageContainer<TMessage, TMetadata> container);
}