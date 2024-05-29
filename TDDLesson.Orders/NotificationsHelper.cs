namespace TDDLesson;

public static class NotificationsHelper
{
   private const int MinimumEmployeesAmountForInvitation = 500;
   
   public static (string, string, string) BuildInvitationalMessage(Proposal proposal, DateOnly processingDate)
   {
      return ("test@mindbox.cloud", "Invitation to forum", "<p>Hello!</p>");
   }

   public static bool ShouldSentNotificationToForum(Proposal proposal, DateOnly processingDate)
   {
      return proposal.EmployeesAmount > MinimumEmployeesAmountForInvitation;
   }
}