namespace TDDLesson;

public class Proposal
{
    private const int MinEmployeesAmount = 100;
    private const float MinITRevenuePercent = 30;
    
    public Proposal(ProposalDto proposalDto)
    {
        if (proposalDto.EmployeesAmount < 0
            || proposalDto.CompanyNumber < 0
            || string.IsNullOrEmpty(proposalDto.CompanyName)
            || string.IsNullOrEmpty(proposalDto.CompanyEmail))
            throw new ArgumentException();

        CompanyNumber = proposalDto.CompanyNumber;
        CompanyName = proposalDto.CompanyName;
        CompanyEmail = proposalDto.CompanyEmail;
        EmployeesAmount = proposalDto.EmployeesAmount;
    }

    public int CompanyNumber { get; }

    public string CompanyName { get; }

    public string CompanyEmail { get; }

    public int EmployeesAmount { get; }

    public bool IsAppropriate(float revenuePercent)
    {
        return EmployeesAmount > MinEmployeesAmount && revenuePercent > MinITRevenuePercent;
    }
}