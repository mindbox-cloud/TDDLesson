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
            EmployeesAmount = 100
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

    [TestMethod]
    [DynamicData(nameof(InvalidProposalDto))]
    public void ShouldThrowArgumentException_WhenNegativeValues(ProposalDto proposalDto)
    {
        //Arrange
        var revenuePercent = 30;

        //Act
        var isApprovedAct = () => proposalDto.IsAppropriate(revenuePercent);

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


    [TestMethod]
    public void ShouldBuildInvitationalMessage_WhenProposalIsAppropriate()
    {
        // Arrange
        var proposal = new ProposalDto
        {
            CompanyNumber = 1,
            CompanyName = "Mindbox",
            CompanyEmail = "test@mindbox.cloud",
            EmployeesAmount = 501
        };

        var processingDateTimeUtc = new DateOnly(2024, 06, 01);

        // Act
        var invitationalMessage = NotificationsHelper.BuildInvitationalMessage(proposal, processingDateTimeUtc);

        // Assert
        invitationalMessage.Should().Be((proposal.CompanyEmail, "Invitation to forum", "<p>Hello!</p>"));
    }
}