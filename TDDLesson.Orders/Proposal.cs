namespace TDDLesson;

public record Proposal
{
    public int CompanyNumber { get; init; }
    public string CompanyName { get; init; } = null!;
    public float RevenuePercent { get; init; }
    public int EmployeesAmount { get; init; }

    public string CompanyEmail { get; init; } = null!;

    public static Proposal Create(int companyNumber, string companyName, float revenuePercent, int employeesAmount, string companyEmail)
    {
        Validate(employeesAmount, revenuePercent);
        
        return new Proposal
        {
            CompanyNumber = companyNumber,
            CompanyName = companyName,
            RevenuePercent = revenuePercent,
            EmployeesAmount = employeesAmount,
            CompanyEmail = companyEmail
        };
    }
    
    private static void Validate(int employeesAmount, float revenuePercent)
    {
        if (employeesAmount <= Constants.EmployeesAmount)
            throw new ArgumentException($"Employees amount must be more than {Constants.EmployeesAmount}");

        if (revenuePercent <= Constants.RevenuePercent)
            throw new ArgumentException($"Revenue percent must be more than {Constants.RevenuePercent}");
    }
}