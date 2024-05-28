namespace TDDLesson;

public static class NotificationService
{
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

        return new EmailDto("test@gmail.com", "test", "test2");
    }
}

