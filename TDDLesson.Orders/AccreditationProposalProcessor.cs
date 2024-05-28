using TDDLesson.Responses;

namespace TDDLesson;

public class AccreditationProposalProcessor(
    IRevenueService revenueService,
    IRepository processedProposalRepository,
    IEmailClient emailClient)
{
    private readonly IRevenueService _revenueService;
    private readonly IRepository _orderRepository;
    private readonly IEmailClient _emailClient;
    
    public async Task<ProcessingResult> HandleProposal(ProposalDto dto)
    {
        var dateTime = DateTime.Now;

        var validationResult = ValidateService.Validate(dto);

        if (validationResult.ValidationStatus == ValidationStatus.Invalid)
        {
            return new ProcessingResult(ProcessingStatus.NotProcessed)
            {
                Message = validationResult.Message
            };
        }

        var revenuePercent = _revenueService.GetRevenuePercent(dto.CompanyNumber);

        var status = StatusEvaluator.Evaluate(dto, dateTime, revenuePercent);

        var (subject, body) = MessageMapper.MapMessage(status);
        emailClient.SendEmail(dto.CompanyEmail, subject, body);

        var processedProposal = new ProcessedProposal(dto, dateTime, status);
        if (status is not ProposalStatus.Declined)
            await processedProposalRepository.SaveAsync(processedProposal);
        
        return new ProcessingResult(ProcessingStatus.Processed);
    }
}