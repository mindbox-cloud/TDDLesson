using FluentAssertions;
using TDDLesson;

namespace TestProject1;

[TestClass]
public class ApproveProposalTest
{
    [TestMethod]
    public void ShouldApproveProposal_HappyPath()
    {
        // Arrange
        var proposal = new Proposal(new ProposalDto
        {
            CompanyNumber = 1,
            CompanyName = "Mindbox",
            CompanyEmail = "test@mindbox.cloud",
            EmployeesAmount = 101
        });

        var revenuePercent = 0.31f;

        // Act
        var isApproved = proposal.IsAppropriate(revenuePercent);

        // Assert
        isApproved.Should().BeTrue();
    }

    [TestMethod]
    public void ShouldNotApproveProposal_WhenNotEnoughEmployees()
    {
        // Arrange
        var proposal = new Proposal(new ProposalDto
        {
            CompanyNumber = 1,
            CompanyName = "Mindbox",
            CompanyEmail = "test@mindbox.cloud",
            EmployeesAmount = 100
        });

        var revenuePercent = 0.31f;

        // Act
        var isApproved = proposal.IsAppropriate(revenuePercent);

        // Assert
        isApproved.Should().BeFalse();
    }

    [TestMethod]
    public void ShouldNotApproveProposal_WhenCompanyHasNotEnoughRevenuePercent()
    {
        // Arrange
        var proposal = new Proposal(new ProposalDto
        {
            CompanyNumber = 1,
            CompanyName = "Mindbox",
            CompanyEmail = "test@mindbox.cloud",
            EmployeesAmount = 101
        });

        var revenuePercent = 0.3f;

        // Act
        var isApproved = proposal.IsAppropriate(revenuePercent);

        // Assert
        isApproved.Should().BeFalse();
    }

    [TestMethod]
    [DynamicData(nameof(InvalidProposalDto))]
    public void ShouldThrowArgumentException_WhenNegativeProposalValues(ProposalDto proposalDto)
    {
        //Arrange
        var revenuePercent = 0.3f;

        //Act
        var isApprovedAct = () => new Proposal(proposalDto).IsAppropriate(revenuePercent);

        //Assert
        isApprovedAct.Should().Throw<ArgumentException>();
    }

    private static IEnumerable<object[]> InvalidProposalDto
    {
        get
        {
            yield return new ProposalDto[]
            {
                new()
                {
                    CompanyNumber = -1,
                    CompanyName = "Mindbox",
                    CompanyEmail = "test@mindbox.cloud",
                    EmployeesAmount = 101
                }
            };
            yield return new ProposalDto[]
            {
                new()
                {
                    CompanyNumber = 1,
                    CompanyName = "Mindbox",
                    CompanyEmail = "test@mindbox.cloud",
                    EmployeesAmount = -1
                }
            };
        }
    }
}