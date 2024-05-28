using TDDLesson.Responses;

namespace TDDLesson;

public static class MessageMapper
{
    public static (string subject, string message) MapMessage(ProposalStatus proposalStatus)
    {
        return proposalStatus switch
        {
            ProposalStatus.Declined => (Messages.DeclinedSubject, Messages.DeclinedBody),
            ProposalStatus.Processed => (Messages.ProcessedSubject, Messages.ProcessedBody),
            ProposalStatus.ProcessedAndInvited => (Messages.InviteSubject, Messages.InviteBody),
            _ => throw new ArgumentOutOfRangeException(nameof(proposalStatus))
        };
    }
}