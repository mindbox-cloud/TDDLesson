using FluentAssertions;

using TDDLesson;

namespace TestProject1;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void ShouldApproveProposal_WhenAllConditionsMet()
    {
        // Arrange
        var proposal = new ProposalDto
        {
            CompanyNumber = 1,
            CompanyName = "Mindbox",
            CompanyEmail = "test@mindbox.cloud",
            EmployeesAmount = 101
        };

        var revenuePercent = 31;
        
        // Act
        var isApproved = proposal.IsAppropriate(revenuePercent);
        
        // Assert
        isApproved.Should().BeTrue();
    }
}