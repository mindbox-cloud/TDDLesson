namespace TDDLesson;

public class AccreditationProposalProcessor
{
    private readonly IRevenueService _revenueService;
    private readonly IRepository _orderRepository;
    private static DateTime MinDate => DateTime.Parse(Constants.MinNotificationDate);
    private static DateTime MaxDate => DateTime.Parse(Constants.MaxNotificationDate);

    public AccreditationProposalProcessor(IRevenueService revenueService, IRepository orderRepository)
    {
        _revenueService = revenueService;
        _orderRepository = orderRepository;
    }
    
    public async Task HandleProposal(ProposalDto dto)
    {
        var validProposal = ValidateProposal(dto.EmployeesAmount, _revenueService.GetRevenuePercent(dto.CompanyNumber));

        if (!validProposal) return;
        
        await _orderRepository.SaveAsync(dto);
        
    }

    public static bool ValidateNotification(DateTime date)
    {
        return date >= MinDate && date <= MaxDate;
    }

    public static bool ValidateProposal(int employeesCount, float percentOfRevenue)
    {
        if (employeesCount < 0)
            throw new ArgumentException("The number of employees cannot be negative.", nameof(employeesCount));

        if (percentOfRevenue < 0)
            throw new ArgumentException("The percent of revenue cannot be negative.", nameof(percentOfRevenue));

        return employeesCount > Constants.EmployeesAmount && percentOfRevenue > Constants.RevenuePercent;
    }
}