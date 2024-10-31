namespace Common.Verification;

public record VerificationResult<TResult>
{
    public VerificationResult()
    {
    }

    public VerificationResult(TResult result)
    {
        Success = true;
        SuccessResult = result;
    }

    public bool Success { get; }

    public TResult? SuccessResult { get; }
}