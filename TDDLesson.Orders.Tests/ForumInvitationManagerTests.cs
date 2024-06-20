using FluentAssertions;
using TDDLesson;

namespace TestProject1;

[TestClass]
public class ForumInvitationManagerTests
{

    [TestMethod]
    public void ShouldSentNotificationToForum_WhenEmployeesAmountIsGreaterThan500()
    {
        // Arrange
        var proposal = new Proposal(new ProposalDto
        {
            CompanyNumber = 1,
            CompanyName = "Mindbox",
            CompanyEmail = "test@mindbox.cloud",
            EmployeesAmount = 501
        });

        var processingDateTimeUtc = new DateOnly(2024, 06, 01);

        // Act
        var invitationalMessage = ForumInvitationManager.ShouldSentNotificationToForum(proposal, processingDateTimeUtc);

        // Assert
        invitationalMessage.Should().BeTrue();
    }

    [TestMethod]
    public void ShouldNotSentNotificationToForum_WhenEmployeesAmountLessOrEqualThan500()
    {
        // Arrange
        var proposal = new Proposal(new ProposalDto
        {
            CompanyNumber = 1,
            CompanyName = "Mindbox",
            CompanyEmail = "test@mindbox.cloud",
            EmployeesAmount = 500
        });

        var processingDateTimeUtc = new DateOnly(2024, 06, 01);

        // Act
        var invitationalMessage = ForumInvitationManager.ShouldSentNotificationToForum(proposal, processingDateTimeUtc);

        // Assert
        invitationalMessage.Should().BeFalse();
    }

    [TestMethod]
    [DataRow("2024-05-31")]
    [DataRow("2024-09-02")]
    public void ShouldNotSentNotificationToForum_WhenProposalProcessingDateIsOutOfRange(string processingDate)
    {
        // Arrange
        var proposal = new Proposal(new ProposalDto
        {
            CompanyNumber = 1,
            CompanyName = "Mindbox",
            CompanyEmail = "test@mindbox.cloud",
            EmployeesAmount = 501
        });

        var processingDateTimeUtc = DateOnly.Parse(processingDate);

        // Act
        var invitationalMessage = ForumInvitationManager.ShouldSentNotificationToForum(proposal, processingDateTimeUtc);

        // Assert
        invitationalMessage.Should().BeFalse();
    }
}