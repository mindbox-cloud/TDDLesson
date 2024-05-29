using FluentAssertions;

namespace TDDLesson;

[TestClass]
public class ProposalTests
{
    [TestMethod]
    public void CreateProposal_CompanyMeetsRequirements_Success()
    {
        var companyNumber = 1;
        var companyName = "TestCompany";
        var revenuePercent = 0.31f;
        var employeesAmount = 101;

        var proposal = Proposal.Create(companyNumber, companyName, revenuePercent, employeesAmount);

        proposal.CompanyNumber.Should().Be(companyNumber);
        proposal.CompanyName.Should().Be(companyName);
        proposal.RevenuePercent.Should().Be(revenuePercent);
        proposal.EmployeesAmount.Should().Be(employeesAmount);
    }
    
    [TestMethod]
    public void CreateProposal_InvalidEmployeesAmount_ThrowsArgumentException()
    {
        var companyNumber = 1;
        var companyName = "TestCompany";
        var revenuePercent = 0.31f;
        var employeesAmount = 99;

        FluentActions.Invoking(() => Proposal.Create(companyNumber, companyName, revenuePercent, employeesAmount))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage($"Employees amount must be more than {Constants.EmployeesAmount}");
    }
    
    [TestMethod]
    public void CreateProposal_InvalidRevenuePercent_ThrowsArgumentException()
    {
        var companyNumber = 1;
        var companyName = "TestCompany";
        var revenuePercent = 0.29f;
        var employeesAmount = 101;

        FluentActions.Invoking(() => Proposal.Create(companyNumber, companyName, revenuePercent, employeesAmount))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage($"Revenue percent must be more than {Constants.RevenuePercent}");
    }
}