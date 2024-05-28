using TDDLesson.Responses;

namespace TDDLesson;

public static class MessageMapper
{
    public static (string subject, string bodyTemplate) MapMessage(ProposalStatus proposalStatus)
    {
        return proposalStatus switch
        {
            ProposalStatus.Declined => (Messages.DeclinedSubject, Messages.DeclinedBodyTemplate),
            ProposalStatus.Processed => (Messages.ProcessedSubject, Messages.ProcessedBodyTemplate),
            ProposalStatus.ProcessedAndInvited => (Messages.InviteSubject, Messages.InviteBodyTemplate),
            _ => throw new ArgumentOutOfRangeException(nameof(proposalStatus))
        };
    }
}