using TDDLesson.Responses;

namespace TDDLesson;

public static class StatusEvaluator
{
    private const int MinAmountOfEmployees = 100;
    private const int MinRequiredEmployeesAmountForInviting = 500;
    private const float MinRevenuePercent = 0.30f;
    private static readonly DateTime StartDate = new(2024, 6, 1);
    private static readonly DateTime EndDate = new(2024, 8, 31);

    public static ProposalStatus Evaluate(ProposalDto proposalDto, DateTime date, float revenuePercent)
    {
        if (!DoesCompanySatisfyRequirements(proposalDto.EmployeesAmount, revenuePercent))
            return ProposalStatus.Declined;
        
        if (!DoesDateSatisfyRange(date))
            return ProposalStatus.Processed;

        return proposalDto.EmployeesAmount > MinRequiredEmployeesAmountForInviting
            ? ProposalStatus.ProcessedAndInvited
            : ProposalStatus.Processed;
    }

    private static bool DoesCompanySatisfyRequirements(int employeesAmount, float revenuePercent)
    {
        return employeesAmount > MinAmountOfEmployees && revenuePercent > MinRevenuePercent;
    }

    private static bool DoesDateSatisfyRange(DateTime date)
    {
        return StartDate <= date && date <= EndDate;
    }
}