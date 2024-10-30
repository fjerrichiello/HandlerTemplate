namespace Common.Validation;


public class ValidationResult
{
    public bool IsValid { get; private set; } = true;
    private readonly List<string> _errorMessages = [];

    public string ErrorMessages => string.Join(' ', _errorMessages);

    public void AddError(string errorMessage)
    {
        IsValid = false;
        _errorMessages.Add(errorMessage);
    }
}
