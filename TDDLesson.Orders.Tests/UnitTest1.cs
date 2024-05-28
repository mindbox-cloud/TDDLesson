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
    
    [TestMethod]
    public void ShouldNotApproveProposal_WhenNotEnoughEmployees()
    {
        // Arrange
        var proposal = new ProposalDto
        {
            CompanyNumber = 1,
            CompanyName = "Mindbox",
            CompanyEmail = "test@mindbox.cloud",
            EmployeesAmount = 99
        };

        var revenuePercent = 31;
        
        // Act
        var isApproved = proposal.IsAppropriate(revenuePercent);
        
        // Assert
        isApproved.Should().BeFalse();
    }
    
    [TestMethod]
    public void ShouldNotApproveProposal_WhenNotEnoughRevenuePercent()
    {
        // Arrange
        var proposal = new ProposalDto
        {
            CompanyNumber = 1,
            CompanyName = "Mindbox",
            CompanyEmail = "test@mindbox.cloud",
            EmployeesAmount = 101
        };

        var revenuePercent = 30;
        
        // Act
        var isApproved = proposal.IsAppropriate(revenuePercent);
        
        // Assert
        isApproved.Should().BeFalse();
    }
}