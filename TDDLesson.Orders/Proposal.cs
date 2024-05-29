namespace TDDLesson;

public record Proposal
{
    public int CompanyNumber { get; init; }
    public string CompanyName { get; init; }
    public float RevenuePercent { get; init; }
    public int EmployeesAmount { get; init; }

    public static Proposal Create(int companyNumber, string companyName, float revenuePercent, int employeesAmount)
    {
        return new Proposal
        {
            CompanyNumber = companyNumber,
            CompanyName = companyName,
            RevenuePercent = revenuePercent,
            EmployeesAmount = employeesAmount
        };
    }
}