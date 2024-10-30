using Common.Mappers;
using Common.Messaging;

namespace Common.Verification;

public abstract class
    Verifier<TMessage, TMetadata, TUnverified, TVerified>(IMapper<TUnverified, TVerified> _mapper) :
    IVerifier<TMessage, TMetadata, TUnverified,
        TVerified>
    where TMessage : Message
    where TMetadata : MessageMetadata
{
    public async Task<VerificationResult<TVerified>> VerifyAsync(
        MessageContainer<TMessage, TMetadata> container, TUnverified data)
    {
        var result = await VerifyInternalAsync(container, data);

        var verificationResult = new VerificationResult<TVerified>();

        if (result)
        {
            verificationResult.SetResult(_mapper.Map(data));
        }

        return verificationResult;
    }

    protected abstract Task<bool> VerifyInternalAsync(MessageContainer<TMessage, TMetadata> container,
        TUnverified data);
}