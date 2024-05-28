namespace TDDLesson;

public static class MessageRenderer
{
    public static string RenderBody(string companyName, string bodyTemplate)
        => bodyTemplate.Replace("{companyName}", companyName);
}