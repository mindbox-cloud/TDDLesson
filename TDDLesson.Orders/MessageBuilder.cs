namespace TDDLesson;

public record EmailMessage(
    string Email,
    string Subject,
    string Body
);

public static class MessageBuilder
{
    public const string ApprovalSubject = "Revenue approved";
    public const string ApprovalBody = "<p>Hello!</p>";
    public static EmailMessage BuildInvitationalMessage(Proposal proposal)
    {

        return new EmailMessage(proposal.CompanyEmail, ForumInvitationManager.InvitationToForumSubject,
            ForumInvitationManager.InvitationToForumBody);
    }

    public static EmailMessage BuildApprovalMessage(Proposal proposal)
    {
        return new EmailMessage(proposal.CompanyEmail, ApprovalSubject, ApprovalBody);
    }
}