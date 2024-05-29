namespace TDDLesson;

public static class NotificationsHelper
{
   public static (string, string, string) BuildInvitationalMessage(Proposal proposal, DateOnly processingDate)
   {
      return ("test@mindbox.cloud", "Invitation to forum", "<p>Hello!</p>");
   }
   
   public static bool ShouldSentNotificationToForum(Proposal proposal, DateOnly processingDate)
   {
      return false;
   }
}