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
        var itRevenuePercent = _revenueService.GetRevenuePercent(dto.CompanyNumber);
        var proposalValidationRequest = new ProposalValidationRequest(
            EmployeesAmount: dto.EmployeesAmount,
            ItRevenuePercent: itRevenuePercent
        );
        
        var valid = ProposalValidator.IsProposalValid(proposalValidationRequest);
        
        if (!valid)
            return;

        var saved = false;
        try
        {
            await _orderRepository.SaveAsync(new ProposalData
            {
                CompanyEmail = dto.CompanyEmail,
                CompanyName = dto.CompanyName,
                CompanyNumber = dto.CompanyNumber,
                EmployeesAmount = dto.EmployeesAmount
            });

            saved = true;
        }
        catch (Exception e)
        {
        }
        
        
        var notificationRequest = new NotificationRequest(
            Saved: saved,
            EmployeesNumber: dto.EmployeesAmount,
            HandleDate: DateTime.UtcNow,
            MailTo: dto.CompanyEmail);

        var emailDto = NotificationService.CreateNotification(notificationRequest);

        if (emailDto != null)
            await _emailClient.SendEmail(
                mailTo: emailDto.MailTo,
                subject: emailDto.Subject,
                body: emailDto.Body);

    }
}