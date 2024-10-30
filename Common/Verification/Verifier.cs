using Common.Mappers;

namespace Common.Verification;

public abstract class
    Verifier<TUnverified, TVerified>(IMapper<TUnverified, TVerified> _mapper) :
    IVerifier<TUnverified,
        TVerified>
{
    public async Task<VerificationResult<TVerified>> VerifyAsync(
        TUnverified data)
    {
        var result = await VerifyInternalAsync(data);

        var verificationResult = new VerificationResult<TVerified>();

        if (result)
        {
            verificationResult.SetResult(_mapper.Map(data));
        }

        return verificationResult;
    }

    protected abstract Task<bool> VerifyInternalAsync(
        TUnverified data);
}