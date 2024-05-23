namespace TDDLesson;

public interface IEmailClient
{
    /// <summary>
    /// Отправляем письмо
    /// </summary>
    /// <param name="mailTo"></param>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    public Task SendEmail(string mailTo, string subject, string body);
}