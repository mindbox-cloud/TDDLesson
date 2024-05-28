using TDDLesson.Responses;

namespace TDDLesson;

public class AccreditationProposalProcessor
{
    private readonly IRevenueService _revenueService;
    private readonly IRepository _processedProposalRepository;
    private readonly IEmailClient _emailClient;

    public AccreditationProposalProcessor(
        IEmailClient emailClient,
        IRepository processedProposalRepository,
        IRevenueService revenueService)
    {
        _emailClient = emailClient;
        _processedProposalRepository = processedProposalRepository;
        _revenueService = revenueService;
    }

    public async Task<ProcessingResult> HandleProposal(ProposalDto dto)
    {
        var dateTime = DateTime.UtcNow;

        var validationResult = ValidationService.Validate(dto);

        if (validationResult.ValidationStatus == ValidationStatus.Invalid)
        {
            return new ProcessingResult(ProcessingStatus.NotProcessed)
            {
                Message = validationResult.Message
            };
        }

        var revenuePercent = _revenueService.GetRevenuePercent(dto.CompanyNumber);

        var status = StatusEvaluator.Evaluate(dto, dateTime, revenuePercent);

        var (subject, bodyTemplate) = MessageMapper.MapMessage(status);
        var body = MessageRenderer.RenderBody(dto.CompanyName, bodyTemplate);

        await _emailClient.SendEmail(dto.CompanyEmail, subject, body);

        var processedProposal = new ProcessedProposal(dto, dateTime, status);
        if (status is not ProposalStatus.Declined)
            await _processedProposalRepository.SaveAsync(processedProposal);
        
        return new ProcessingResult(ProcessingStatus.Processed);
    }
}