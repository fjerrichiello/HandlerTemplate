using Common.Messaging;

namespace Common.Verification;

public interface IVerifier<in TUnverified, TVerified>
{
    Task<VerificationResult<TVerified>> VerifyAsync(
        TUnverified data);
}