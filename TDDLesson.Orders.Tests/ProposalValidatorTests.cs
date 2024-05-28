using FluentAssertions;
using TDDLesson;

namespace TestProject1;

[TestClass]
public class ProposalValidatorTests
{
    [TestMethod]
    public void ShouldBeValid_WhenEmployeesAndRevenueInCorrectInterval()
    {
        var request = new ProposalValidationRequest(101, 31);

        var result = ProposalValidator.IsProposalValid(request);

        result.Should().BeTrue();
    }
    
    [TestMethod]
    [DataRow(100)]
    [DataRow(50)]
    public void ShouldBeInvalid_WhenEmployeesNumberLessThan100(int employeeAmount)
    {
        var request = new ProposalValidationRequest(employeeAmount, 31);

        var result = ProposalValidator.IsProposalValid(request);

        result.Should().BeFalse();
    }
    
    [TestMethod]
    [DataRow(30)]
    [DataRow(15)]
    public void ShouldBeInvalid_WhenRevenueAmountLessThan30(int itRevenuePercent)
    {
        var request = new ProposalValidationRequest(101, itRevenuePercent);

        var result = ProposalValidator.IsProposalValid(request);

        result.Should().BeFalse();
    }
}