using FluentAssertions;
using TDDLesson;

namespace TestProject1;

[TestClass]
public class MessageBuilderTests
{
    [TestMethod]
    public void ShouldBuildInvitationalMessage()
    {
        // Arrange
        var proposal = new Proposal(new ProposalDto
        {
            CompanyNumber = 1,
            CompanyName = "Mindbox",
            CompanyEmail = "test@mindbox.cloud",
            EmployeesAmount = 501
        });

        // Act
        var invitationalMessage = MessageBuilder.BuildInvitationalMessage(proposal);

        // Assert
        invitationalMessage.Should()
            .Be(new EmailMessage(proposal.CompanyEmail, "Invitation to forum", "<p>Hello!</p>"));
    }

    [TestMethod]
    public void ShouldBuildApprovalMessage()
    {
        // Arrange
        var proposal = new Proposal(new ProposalDto
        {
            CompanyNumber = 1,
            CompanyName = "Mindbox",
            CompanyEmail = "test@mindbox.cloud",
            EmployeesAmount = 501
        });

        // Act
        var invitationalMessage = MessageBuilder.BuildApprovalMessage(proposal);

        // Assert
        invitationalMessage.Should()
            .Be(new EmailMessage(proposal.CompanyEmail, "Proposal approved", "<p>Hello!</p>"));
    }
}