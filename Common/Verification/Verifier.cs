namespace Common.Verification;

public abstract class
    Verifier<TUnverified, TVerified> :
    IVerifier<TUnverified,
        TVerified>
{
    public async Task<VerificationResult<TVerified>> VerifyAsync(
        TUnverified data)
    {
        return await VerifyInternalAsync(data);
    }

    protected abstract Task<VerificationResult<TVerified>> VerifyInternalAsync(
        TUnverified data);
}