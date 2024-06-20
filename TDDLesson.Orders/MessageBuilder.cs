namespace TDDLesson;

public record EmailMessage(
    string Email,
    string Subject,
    string Body
);

public static class MessageBuilder
{
    public const string InvitationToForumSubject = "Invitation to forum";
    public const string InvitationToForumBody = "<p>Hello!</p>";

    public const string ApprovalSubject = "Proposal approved";
    public const string ApprovalBody = "<p>Hello!</p>";
    
    public static EmailMessage BuildInvitationalMessage(Proposal proposal)
    {
        return new EmailMessage(proposal.CompanyEmail, InvitationToForumSubject,
            InvitationToForumBody);
    }

    public static EmailMessage BuildApprovalMessage(Proposal proposal)
    {
        return new EmailMessage(proposal.CompanyEmail, ApprovalSubject, ApprovalBody);
    }
}