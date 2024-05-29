namespace TDDLesson;

public class Proposal
{
    private const int MinEmployeesAmount = 100;
    private const float MinITRevenuePercent = 0.3f;
    
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
        Validate(revenuePercent);
        return EmployeesAmount > MinEmployeesAmount && revenuePercent > MinITRevenuePercent;
    }
    private static void Validate(float revenuePercent)
    {
        if (revenuePercent is < 0 or > 1)
        {
            throw new ArgumentException("Incorrect revenue percent");
        }
    }
}