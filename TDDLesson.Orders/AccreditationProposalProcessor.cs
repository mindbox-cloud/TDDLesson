namespace TDDLesson;

public class AccreditationProposalProcessor
{
    private readonly IRevenueService _revenueService;
    private readonly IRepository _proposalRepository;
    private readonly IEmailClient _emailClient;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AccreditationProposalProcessor(
        IRevenueService revenueService,
        IRepository proposalRepository,
        IEmailClient emailClient,
        IDateTimeProvider dateTimeProvider)
    {
        _revenueService = revenueService;
        _proposalRepository = proposalRepository;
        _emailClient = emailClient;
        _dateTimeProvider = dateTimeProvider;
    }
    
    public async Task HandleProposal(ProposalDto dto)
    {
        var proposal = new Proposal(dto);
        _proposalRepository.SaveAsync(proposal);
        var revenue = _revenueService.GetRevenuePercent(proposal.CompanyNumber);
        if (proposal.IsAppropriate(revenue))
        {
            var approvalMessage = MessageBuilder.BuildApprovalMessage(proposal);
            _emailClient.SendEmail(approvalMessage.Email, approvalMessage.Subject, approvalMessage.Body);
        }
        if (ForumInvitationManager.ShouldSentNotificationToForum(proposal, DateOnly.FromDateTime(_dateTimeProvider.GetDateTimeUtcNow())))
        {
            var approvalMessage = MessageBuilder.BuildInvitationalMessage(proposal);
            _emailClient.SendEmail(approvalMessage.Email, approvalMessage.Subject, approvalMessage.Body);
        }
    }
}