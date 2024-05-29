using FluentAssertions;
using Moq;
using TDDLesson;

namespace TestProject1;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void ShouldBuildInvitationalMessage_WhenProposalIsAppropriate()
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
        var invitationalMessage = MessageBuilder.BuildInvitationalMessage(proposal);

        // Assert
        invitationalMessage.Should().Be((proposal.CompanyEmail, "Invitation to forum", "<p>Hello!</p>"));
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

    //this is should be integration test
    [TestMethod]
    public async Task ShouldProposalProcessed_HappyPath()
    {
        // Arrange
        var proposalDto = new ProposalDto
        {
            CompanyNumber = 1,
            CompanyName = "Mindbox",
            CompanyEmail = "test@mindbox.cloud",
            EmployeesAmount = 501
        };
        var revenueServiceMock = new Mock<IRevenueService>();
        revenueServiceMock.Setup(r => r.GetRevenuePercent(proposalDto.CompanyNumber)).Returns(31);
        var revenueService = revenueServiceMock.Object;
        var orderRepositoryMock = new Mock<IRepository>();
        var orderRepository = orderRepositoryMock.Object;
        var emailClientMock = new Mock<IEmailClient>();
        var emailClient = emailClientMock.Object;
        var dateTimeMock = new Mock<IDateTimeProvider>();
        dateTimeMock.Setup(p => p.GetDateTimeUtcNow())
            .Returns(new DateTime(2024, 06, 02));


        var processor = new AccreditationProposalProcessor(
            revenueService,
            orderRepository,
            emailClient,
            dateTimeMock.Object);

        //Act
        await processor.HandleProposal(proposalDto);

        //Assert
        emailClientMock.Verify(
            c => c.SendEmail("test@mindbox.cloud", ForumInvitationManager.InvitationToForumSubject,
                ForumInvitationManager.InvitationToForumBody), Times.Once);
        emailClientMock.Verify(
            c => c.SendEmail("test@mindbox.cloud", MessageBuilder.ApprovalSubject, MessageBuilder.ApprovalBody),
            Times.Once);

        orderRepositoryMock.Verify(r => r.SaveAsync(It.Is<Proposal>(p =>
            p.CompanyName == proposalDto.CompanyName
            && p.CompanyEmail == proposalDto.CompanyEmail
            && p.EmployeesAmount == proposalDto.EmployeesAmount
            && p.CompanyNumber == proposalDto.CompanyNumber)));

        revenueServiceMock.Verify(r => r.GetRevenuePercent(proposalDto.CompanyNumber), Times.Once);
    }
}