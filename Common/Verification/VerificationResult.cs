namespace Common.Verification;

public class VerificationResult<TResult>
{
    public bool Success { get; private set; } = true;

    public TResult SuccessResult { get; set; }
}