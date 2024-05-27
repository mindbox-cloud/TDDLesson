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
        
    }

    public static bool Validate(int employeesCount, float percentOfRevenue)
    {
        if (employeesCount < 0)
            throw new ArgumentException("The number of employees cannot be negative.", nameof(employeesCount));

        if (percentOfRevenue < 0)
            throw new ArgumentException("The percent of revenue cannot be negative.", nameof(percentOfRevenue));

        return employeesCount > Constants.EmployeesAmount && percentOfRevenue > Constants.RevenuePercent;
    }
}