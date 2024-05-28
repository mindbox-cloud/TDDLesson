namespace TDDLesson;

public record ValidationResult(ValidationStatus ValidationStatus)
{
    public string? Message { get; init; }
}