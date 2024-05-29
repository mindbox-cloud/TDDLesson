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
        ValidateProposalDto(dto);

        var revenuePercent = _revenueService.GetRevenuePercent(dto.CompanyNumber);

        var proposal = Proposal.Create(dto.CompanyNumber, dto.CompanyName, revenuePercent, dto.EmployeesAmount, dto.CompanyEmail);
        
        await _orderRepository.SaveAsync(proposal);

        var emails = EmailService.GetEmailsForProposal(proposal, DateTime.UtcNow);
        foreach (var email in emails)
        {
            await _emailClient.SendEmail(email.Recipient, email.Subject, email.Body);
        }
    }

    private static void ValidateProposalDto(ProposalDto proposalDto)
    {
        if (proposalDto.EmployeesAmount < 0)
            throw new ArgumentException("The number of employees cannot be negative.", nameof(proposalDto));

        if (proposalDto.CompanyNumber < 0)
            throw new ArgumentException("Company number cannot be negative.", nameof(proposalDto));

        ArgumentException.ThrowIfNullOrWhiteSpace(proposalDto.CompanyEmail, nameof(proposalDto));
        ArgumentException.ThrowIfNullOrWhiteSpace(proposalDto.CompanyName, nameof(proposalDto));
    }
}