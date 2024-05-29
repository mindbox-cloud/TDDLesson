using FluentAssertions;

namespace TDDLesson;

[TestClass]
public class ProposalTests
{
    private const int CompanyNumber = 1;
    private const string CompanyName = "TestCompany";
    private const string CompanyEmail = "test@email.ru";
    
    [TestMethod]
    [DataRow(101, 0.31f)]
    [DataRow(105, 0.45f)]
    [DataRow(1000, 1f)]
    public void CreateProposal_CompanyMeetsRequirements_Success(int employeesAmount, float revenuePercent)
    {
        var proposal = Proposal.Create(CompanyNumber, CompanyName, revenuePercent, employeesAmount, CompanyEmail);

        proposal.CompanyNumber.Should().Be(CompanyNumber);
        proposal.CompanyName.Should().Be(CompanyName);
        proposal.RevenuePercent.Should().Be(revenuePercent);
        proposal.EmployeesAmount.Should().Be(employeesAmount);
    }
    
    [TestMethod]
    [DataRow(100, 0.5f)]
    [DataRow(50, 0.5f)]
    public void CreateProposal_InvalidEmployeesAmount_ThrowsArgumentException(int employeesAmount, float revenuePercent)
    {
        FluentActions.Invoking(() => Proposal.Create(CompanyNumber, CompanyName, revenuePercent, employeesAmount, CompanyEmail))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage($"Employees amount must be more than {Constants.EmployeesAmount}");
    }
    
    [TestMethod]
    [DataRow(500, 0.3f)]
    [DataRow(500, 0.1f)]
    public void CreateProposal_InvalidRevenuePercent_ThrowsArgumentException(int employeesAmount, float revenuePercent)
    {
        FluentActions.Invoking(() => Proposal.Create(CompanyNumber, CompanyName, revenuePercent, employeesAmount, CompanyEmail))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage($"Revenue percent must be more than {Constants.RevenuePercent}");
    }
}