using FluentAssertions;
using TDDLesson;

namespace TestProject1;

[TestClass]
public class ProposalValidatorTests
{
    private readonly ProposalValidator _proposalValidator = new();
    
    [TestMethod]
    public void IsProposalValid_IsValid_True()
    {
        var request = new ProposalValidationRequest(101, 31);

        var result = _proposalValidator.IsProposalValid(request);

        result.Should().BeTrue();
    }
    
    [TestMethod]
    [DataRow(100)]
    [DataRow(50)]
    public void IsProposalValid_EmployeeAmountIsInvalid_False(int employeeAmount)
    {
        var request = new ProposalValidationRequest(employeeAmount, 31);

        var result = _proposalValidator.IsProposalValid(request);

        result.Should().BeFalse();
    }
    
    [TestMethod]
    [DataRow(30)]
    [DataRow(15)]
    public void IsProposalValid_ItRevenuePercentIsInvalid_False(int itRevenuePercent)
    {
        var request = new ProposalValidationRequest(101, itRevenuePercent);

        var result = _proposalValidator.IsProposalValid(request);

        result.Should().BeFalse();
    }
}