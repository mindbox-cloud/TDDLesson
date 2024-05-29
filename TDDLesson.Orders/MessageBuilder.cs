namespace TDDLesson;

public static class MessageBuilder
{
    public const string ApprovalSubject = "Revenue approved";
    public const string ApprovalBody = "<p>Hello!</p>";
    public static (string, string, string) BuildInvitationalMessage(Proposal proposal)
    {

        return (proposal.CompanyEmail, ForumInvitationManager.InvitationToForumSubject,
            ForumInvitationManager.InvitationToForumBody);
    }

    public static (string, string, string) BuildApprovalMessage(Proposal proposal)
    {
        return (proposal.CompanyEmail, ApprovalSubject, ApprovalBody);
    }
}