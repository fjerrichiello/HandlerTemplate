using Common.Messaging;

namespace Common.Verification;

public interface IVerifier<TMessage, TMetadata, in TUnverified, TVerified>
    where TMessage : Message
    where TMetadata : MessageMetadata
{
    Task<VerificationResult<TVerified>> VerifyAsync(MessageContainer<TMessage, TMetadata> container,
        TUnverified data);
}