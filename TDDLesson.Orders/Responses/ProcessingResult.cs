namespace TDDLesson.Responses;

public record ProcessingResult(ProcessingStatus ProcessingStatus)
{
    public string? Message { get; init; }
}

public enum ProcessingStatus
{
    Processed,
    NotProcessed
}