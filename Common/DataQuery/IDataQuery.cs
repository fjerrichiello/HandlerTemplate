using Common.Messaging;

namespace Common.DataQuery;

public interface IDataQuery<TMessage, TMetadata>
    where TMessage : Message
    where TMetadata : MessageMetadata
{
    Task<TUnverified> GetDataAsync<TUnverified>(
        MessageContainer<TMessage, TMetadata> container);
}