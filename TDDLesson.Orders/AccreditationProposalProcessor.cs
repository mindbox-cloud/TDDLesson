namespace TDDLesson;

public class AccreditationProposalProcessor
{
    private readonly IRevenueService _revenueService;
    private readonly IRepository _orderRepository;
    private readonly IEmailClient _emailClient;

    public AccreditationProposalProcessor(
        IRevenueService revenueService,
        IRepository orderRepository,
        IEmailClient emailClient)
    {
        _revenueService = revenueService;
        _orderRepository = orderRepository;
        _emailClient = _emailClient;
    }
    
    public async Task HandleProposal(ProposalDto dto)
    {
        var proposal = new Proposal(dto);
        
    }
}