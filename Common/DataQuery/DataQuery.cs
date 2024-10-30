using Common.Messaging;

namespace Common.DataQuery;

public abstract class
    DataQuery<TMessage, TMetadata, TUnverified> :
    IDataQuery<TMessage,
        TMetadata, TUnverified>
    where TMessage : Message
    where TMetadata : MessageMetadata
{
    public async Task<TUnverified> GetDataAsync(MessageContainer<TMessage, TMetadata> container)
    {
        return await GetDataInternalAsync(container);
    }

    protected abstract Task<TUnverified> GetDataInternalAsync(
        MessageContainer<TMessage, TMetadata> container);
}