namespace TDDLesson;

public class AccreditationProposalProcessor(
    IRevenueService revenueService,
    IRepository proposalRepository,
    IEmailClient emailClient,
    IDateTimeProvider dateTimeProvider)
{
    public async Task HandleProposal(ProposalDto dto)
    {
        var proposal = new Proposal(dto);
        await proposalRepository.SaveAsync(proposal);
        
        var revenue = revenueService.GetRevenuePercent(proposal.CompanyNumber);
        
        if (proposal.IsAppropriate(revenue))
        {
            var approvalMessage = MessageBuilder.BuildApprovalMessage(proposal);
            await emailClient.SendEmail(approvalMessage.Email, approvalMessage.Subject, approvalMessage.Body);
        }

        if (ForumInvitationManager.ShouldSentNotificationToForum(proposal,
                DateOnly.FromDateTime(dateTimeProvider.GetDateTimeUtcNow())))
        {
            var approvalMessage = MessageBuilder.BuildInvitationalMessage(proposal);
            await emailClient.SendEmail(approvalMessage.Email, approvalMessage.Subject, approvalMessage.Body);
        }
    }
}