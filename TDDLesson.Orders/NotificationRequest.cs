namespace TDDLesson;

public record NotificationRequest(bool Saved, int EmployeesNumber, DateTime HandleDate, string MailTo);