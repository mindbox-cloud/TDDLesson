namespace TDDLesson;

public class AccreditationProposalProcessor
{
    private readonly IRevenueService _revenueService;
    private readonly IRepository _orderRepository;
    private readonly IEmailClient _emailClient;

    public AccreditationProposalProcessor(IRevenueService revenueService, IRepository orderRepository, IEmailClient emailClient)
    {
        _revenueService = revenueService;
        _orderRepository = orderRepository;
        _emailClient = emailClient;
    }
    
    public async Task HandleProposal(ProposalDto dto)
    {
        var utcNow = DateTime.UtcNow;

        ValidateService.Validate(dto);

        var revenuePercent = _revenueService.GetRevenuePercent(dto.CompanyNumber);

        var status = StatusEvaluator.Evaluate(dto, utcNow, revenuePercent);

        var (subject, body) = MessageMapper.MapMessage(status);

        _emailClient.SendEmail(dto.CompanyEmail, subject, body);
    }
}