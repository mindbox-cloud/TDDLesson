namespace TDDLesson;

public static class NotificationsHelper
{
   public static (string, string, string) BuildInvitationalMessage(ProposalDto proposalDto, DateOnly processingDate)
   {
      return ("test@mindbox.cloud", "Invitation to forum", "<p>Hello!</p>");
   }
}