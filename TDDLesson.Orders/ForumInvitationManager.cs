namespace TDDLesson;

public static class ForumInvitationManager
{
    public static string InvitationToForumSubject = "Invitation to forum";
    public static string InvitationToForumBody = "<p>Hello!</p>";
        
    private const int MinimumEmployeesAmountForInvitation = 500;
    private static DateOnly RightBound = new(2024, 09, 01);
    private static DateOnly LeftBound = new(2024, 06, 01);
    
    public static bool ShouldSentNotificationToForum(Proposal proposal, DateOnly processingDate)
    {
        return proposal.EmployeesAmount > MinimumEmployeesAmountForInvitation
               && processingDate <= RightBound
               && processingDate >= LeftBound;
    }
}