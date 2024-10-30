namespace Common.Verification;

public class VerificationResult<TResult>
{
    public bool Success { get; private set; }

    public TResult? SuccessResult { get; private set; }

    public void SetResult(TResult successResult)
    {
        Success = true;
        SuccessResult = successResult;
    }
}