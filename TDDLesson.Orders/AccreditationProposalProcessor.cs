using System.Data;

namespace TDDLesson;

public class AccreditationProposalProcessor
{
    private readonly IRevenueService _revenueService;
    private readonly IRepository _orderRepository;
    private readonly IEmailClient _emailClient;
    private static DateTime MinDate => DateTime.Parse(Constants.MinNotificationDate);
    private static DateTime MaxDate => DateTime.Parse(Constants.MaxNotificationDate);

    public AccreditationProposalProcessor(IRevenueService revenueService, IRepository orderRepository, IEmailClient emailClient)
    {
        _revenueService = revenueService;
        _orderRepository = orderRepository;
        _emailClient = emailClient;
    }
    
    public async Task HandleProposal(ProposalDto dto)
    {
        ValidateProposalDto(dto);

        var revenuePercent = _revenueService.GetRevenuePercent(dto.CompanyNumber);
        
        var validProposal = ValidateProposal(dto.EmployeesAmount, _revenueService.GetRevenuePercent(dto.CompanyNumber));

        if (!validProposal) return;
        
        await _orderRepository.SaveAsync(dto);

        if (ValidateNotification(DateTime.UtcNow))
        {
            await _emailClient.SendEmail(dto.CompanyEmail, Constants.SuccessOrderProcessingSubject,  Constants.SuccessOrderProcessingBody);

            if (ShouldSendInvitation(dto.EmployeesAmount))
            {
                await _emailClient.SendEmail(dto.CompanyEmail, Constants.ForumInvitationSubject,
                    $"{dto.CompanyName}! " + Constants.ForumInvitationBody);
            }
        }
    }

    public static bool ValidateNotification(DateTime date)
    {
        return date >= MinDate && date <= MaxDate;
    }

    public static void ValidateProposalDto(ProposalDto proposalDto)
    {
        if (proposalDto.EmployeesAmount < 0)
            throw new ArgumentException("The number of employees cannot be negative.", nameof(proposalDto));

        if (proposalDto.CompanyNumber < 0)
            throw new ArgumentException("Company number cannot be negative.", nameof(proposalDto));
    }

    public static bool ValidateProposal(int employeesCount, float percentOfRevenue)
    {
        if (employeesCount < 0)
            throw new ArgumentException("The number of employees cannot be negative.", nameof(employeesCount));

        if (percentOfRevenue < 0)
            throw new ArgumentException("The percent of revenue cannot be negative.", nameof(percentOfRevenue));

        return employeesCount > Constants.EmployeesAmount && percentOfRevenue > Constants.RevenuePercent;
    }

    public static bool ShouldSendInvitation(int employeeCount)
    {
        return employeeCount > Constants.EmployeesAmountForInvitation;
    }
}