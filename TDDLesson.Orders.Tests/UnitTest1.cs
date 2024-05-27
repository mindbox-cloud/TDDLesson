using FluentAssertions;

namespace TDDLesson;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    [DataRow(105, 45)]
    [DataRow(101, 31)]
    [DataRow(1000, 100)]
    public void ValidateProposal_ValidParameters_Success(int employeesCount, float percentOfRevenue)
    {
        var shouldSave = AccreditationProposalProcessor.Validate(employeesCount, percentOfRevenue);
        
        shouldSave.Should().BeTrue();
    }
}