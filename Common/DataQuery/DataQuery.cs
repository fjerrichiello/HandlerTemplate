using Common.Messaging;

namespace Common.DataQuery;

public abstract class
    DataQuery<TMessage, TMetadata> :
    IDataQuery<TMessage,
        TMetadata>
    where TMessage : Message
    where TMetadata : MessageMetadata
{
    public async Task<TUnverified> GetDataAsync<TUnverified>(MessageContainer<TMessage, TMetadata> container)
    {
        return await GetDataInternalAsync<TUnverified>(container);
    }

    protected abstract Task<TUnverified> GetDataInternalAsync<TUnverified>(
        MessageContainer<TMessage, TMetadata> container);
}