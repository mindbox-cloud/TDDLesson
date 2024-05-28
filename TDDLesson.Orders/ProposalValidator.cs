namespace TDDLesson;

public static class ProposalValidator
{
    public static bool IsProposalValid(ProposalValidationRequest proposalValidationRequest)
    {
        if (proposalValidationRequest.EmployeesAmount <= 100)
            return false;
        
        if (proposalValidationRequest.ItRevenuePercent <= 30)
            return false;
        
        return true;
    }
}