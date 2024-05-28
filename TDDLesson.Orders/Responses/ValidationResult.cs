namespace TDDLesson.Responses;

public record ValidationResult(ValidationStatus ValidationStatus)
{
    public string? Message { get; init; }
}

public enum ValidationStatus
{
    Valid,
    Invalid
}