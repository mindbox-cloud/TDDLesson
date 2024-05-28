namespace TDDLesson.Orders.Tests;

public static class TestHelper
{
    public static ProposalDto CreateProposalDto(
        int companyNumber = 1,
        string companyName = "Test Company",
        string companyEmail = "test@example.com",
        int employeesAmount = 101)
    {
        return new ProposalDto
        {
            CompanyNumber = companyNumber,
            CompanyName = companyName,
            CompanyEmail = companyEmail,
            EmployeesAmount = employeesAmount
        };
    }
}