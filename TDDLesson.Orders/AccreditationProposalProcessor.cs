namespace TDDLesson;

public class AccreditationProposalProcessor(
    IRevenueService revenueService,
    IRepository processedProposalRepository,
    IEmailClient emailClient)
{
    public async Task HandleProposal(ProposalDto dto)
    {
        var dateTime = DateTime.Now;

        ValidateService.Validate(dto);

        var revenuePercent = revenueService.GetRevenuePercent(dto.CompanyNumber);

        var status = StatusEvaluator.Evaluate(dto, dateTime, revenuePercent);

        var (subject, body) = MessageMapper.MapMessage(status);

        emailClient.SendEmail(dto.CompanyEmail, subject, body);

        var processedProposal = new ProcessedProposal(dto, dateTime, status);
        if (status is not ProposalStatus.Declined)
            await processedProposalRepository.SaveAsync(processedProposal);
    }
}