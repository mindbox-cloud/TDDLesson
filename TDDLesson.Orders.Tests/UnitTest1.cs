using FluentAssertions;

namespace TDDLesson;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    [DataRow(105, 0.45f)]
    [DataRow(101, 0.31f)]
    [DataRow(1000, 1f)]
    public void ValidateProposal_ValidParameters_Success(int employeesCount, float percentOfRevenue)
    {
        var shouldSave = AccreditationProposalProcessor.Validate(employeesCount, percentOfRevenue);
        
        shouldSave.Should().BeTrue();
    }

    [TestMethod]
    [DataRow(100, 0.3f)]
    [DataRow(100, 0.5f)]
    [DataRow(500, 0.3f)]
    [DataRow(50, 0.1f)]
    [DataRow(500, 0.1f)]
    [DataRow(50, 0.5f)]
    [DataRow(0, 0f)]
    public void ValidateProposal_InvalidParameters_Failed(int employeesCount, float percentOfRevenue)
    {
        var shouldSave = AccreditationProposalProcessor.Validate(employeesCount, percentOfRevenue);
        
        shouldSave.Should().BeFalse();
    }

    [TestMethod]
    [DataRow(-100, -0.3f)]
    [DataRow(-100, 0.3f)]
    public void ValidateProposal_NegativeEmployeesCount_ThrowsArgumentException(int employeesCount, float percentOfRevenue)
    {
        FluentActions.Invoking(() => AccreditationProposalProcessor.Validate(employeesCount, percentOfRevenue))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage($"The number of employees cannot be negative. (Parameter '{nameof(employeesCount)}')");
    }

    [TestMethod]
    [DataRow(100, -0.3f)]
    public void ValidateProposal_NegativeRevenuePercent_ThrowsArgumentException(int employeesCount, float percentOfRevenue)
    {
        FluentActions.Invoking(() => AccreditationProposalProcessor.Validate(employeesCount, percentOfRevenue))
            .Should()
            .Throw<ArgumentException>()
            .WithMessage($"The percent of revenue cannot be negative. (Parameter '{nameof(percentOfRevenue)}')");
    }
}