namespace TDDLesson;

public static class NotificationService
{
    public const string Subject = "Proposal info.";
    public const string ProcessedMessage = "Proposal processed.";
    public const string InviteMessage = "We invite you to the forum on the development of the IT industry.";
    
    public static EmailDto? CreateNotification(NotificationRequest request)
    {
        if (request.HandleDate < new DateTime(2024, 06, 01) || request.HandleDate > new DateTime(2024, 09, 01))
        {
            return null;
        }
        if (request.EmployeesNumber <= 100)
        {
            return null;
        }

        var body = request.EmployeesNumber <= 500 ? ProcessedMessage : ProcessedMessage + " " + InviteMessage;
        return new EmailDto(request.MailTo, Subject, body);
    }
}

