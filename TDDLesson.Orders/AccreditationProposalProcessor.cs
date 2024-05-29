namespace TDDLesson;

public class AccreditationProposalProcessor
{
    private readonly IRevenueService _revenueService;
    private readonly IRepository _orderRepository;
    private readonly IEmailClient _emailClient;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AccreditationProposalProcessor(
        IRevenueService revenueService,
        IRepository orderRepository,
        IEmailClient emailClient,
        IDateTimeProvider dateTimeProvider)
    {
        _revenueService = revenueService;
        _orderRepository = orderRepository;
        _emailClient = emailClient;
        _dateTimeProvider = dateTimeProvider;
    }
    
    public async Task HandleProposal(ProposalDto dto)
    {
        var proposal = new Proposal(dto);
        var revenue = _revenueService.GetRevenuePercent(proposal.CompanyNumber);
        if (proposal.IsAppropriate(revenue))
        {
            _orderRepository.SaveAsync(proposal);
            var approvalMessage = NotificationsHelper.BuildApprovalMessage(proposal);
            _emailClient.SendEmail(approvalMessage.Item1, approvalMessage.Item2, approvalMessage.Item3);
        }
        if (NotificationsHelper.ShouldSentNotificationToForum(proposal, DateOnly.FromDateTime(_dateTimeProvider.GetDateTimeUtcNow())))
        {
            
            var approvalMessage = NotificationsHelper.BuildInvitationalMessage(proposal);
            _emailClient.SendEmail(approvalMessage.Item1, approvalMessage.Item2, approvalMessage.Item3);
        }
        
        
    }
    
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetDateTimeUtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}

public interface IDateTimeProvider
{
    DateTime GetDateTimeUtcNow();
}