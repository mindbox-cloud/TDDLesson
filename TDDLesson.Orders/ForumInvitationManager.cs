namespace TDDLesson;

public static class ForumInvitationManager
{
    private const int MinimumEmployeesAmountForInvitation = 500;
    private static readonly DateOnly RightBound = new(2024, 09, 01);
    private static readonly DateOnly LeftBound = new(2024, 06, 01);
    
    public static bool ShouldSentNotificationToForum(Proposal proposal, DateOnly processingDate)
    {
        return proposal.EmployeesAmount > MinimumEmployeesAmountForInvitation
               && processingDate <= RightBound
               && processingDate >= LeftBound;
    }
}