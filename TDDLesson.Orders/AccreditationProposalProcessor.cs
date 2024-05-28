namespace TDDLesson;

public class AccreditationProposalProcessor
{
    private readonly IRevenueService _revenueService;
    private readonly IRepository _orderRepository;

    public AccreditationProposalProcessor(IRevenueService revenueService, IRepository orderRepository)
    {
        _revenueService = revenueService;
        _orderRepository = orderRepository;
    }
    
    public async Task HandleProposal(ProposalDto dto)
    {
        // ValidateService.Validate(dto)
        
        // RevenueService.GetRevenuePercent(int companyNumber)
        
        // StatusEvaluator.Evaluate(proposalDto, revenue, datetime)
        
        // NotificationRenderer.Render(proposalDto, status)
        
        // IEmailClient.SendEmail(string mailTo, string subject, string body)
        
        // Repository.Save(entity)
    }
}