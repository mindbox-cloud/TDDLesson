namespace TDDLesson;

public class ProposalValidator
{
    public bool IsProposalValid(ProposalValidationRequest proposalValidationRequest)
    {
        if (proposalValidationRequest.EmployeesAmount <= 100)
            return false;
        
        if (proposalValidationRequest.ItRevenuePercent <= 30)
            return false;
        
        return true;
    }
}