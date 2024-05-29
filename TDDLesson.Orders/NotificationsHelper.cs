namespace TDDLesson;

public static class NotificationsHelper
{
   private const int MinimumEmployeesAmountForInvitation = 500;
   private static DateOnly RightBound = new DateOnly(2024, 09, 01);
   private static DateOnly LeftBound = new DateOnly(2024, 06, 01);
   
   public static (string, string, string) BuildInvitationalMessage(Proposal proposal)
   {
      return (proposal.CompanyEmail, "Invitation to forum", "<p>Hello!</p>");
   }
   
   public static (string, string, string) BuildApprovalMessage(Proposal proposal)
   {
      return (proposal.CompanyEmail, "Revenue approved", "<p>Hello!</p>");
   }

   public static bool ShouldSentNotificationToForum(Proposal proposal, DateOnly processingDate)
   {
      return proposal.EmployeesAmount > MinimumEmployeesAmountForInvitation
         && processingDate <= RightBound
         && processingDate >= LeftBound;
   }
}