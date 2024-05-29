namespace TDDLesson;

public record Email
{
    public string Recipient { get; init; } = null!;
    public string Subject { get; init; } = null!;
    public string Body { get; init; } = null!;

    public Email(string recipient, string subject, string body)
    {
        Recipient = recipient;
        Subject = subject;
        Body = body;
    }
}