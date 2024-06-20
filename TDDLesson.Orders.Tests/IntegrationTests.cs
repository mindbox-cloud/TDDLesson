using Moq;
using TDDLesson;

namespace TestProject1;

//this should be integration test
[TestClass]
public class IntegrationTests
{
    [TestMethod]
    public async Task ShouldProcessProposal_HappyPath()
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
        revenueServiceMock.Setup(r => r.GetRevenuePercent(proposalDto.CompanyNumber)).Returns(.31f);
        var proposalRepositoryMock = new Mock<IRepository>();
        var emailClientMock = new Mock<IEmailClient>();
        var dateTimeMock = new Mock<IDateTimeProvider>();
        dateTimeMock.Setup(p => p.GetDateTimeUtcNow())
            .Returns(new DateTime(2024, 06, 02));

        var processor = new AccreditationProposalProcessor(
            revenueServiceMock.Object,
            proposalRepositoryMock.Object,
            emailClientMock.Object,
            dateTimeMock.Object);

        //Act
        await processor.HandleProposal(proposalDto);

        //Assert
        emailClientMock.Verify(
            c => c.SendEmail(proposalDto.CompanyEmail, MessageBuilder.InvitationToForumSubject,
                MessageBuilder.InvitationToForumBody), Times.Once);
        emailClientMock.Verify(
            c => c.SendEmail(proposalDto.CompanyEmail, MessageBuilder.ApprovalSubject, MessageBuilder.ApprovalBody),
            Times.Once);

        proposalRepositoryMock.Verify(r => r.SaveAsync(It.Is<Proposal>(p =>
            p.CompanyName == proposalDto.CompanyName
            && p.CompanyEmail == proposalDto.CompanyEmail
            && p.EmployeesAmount == proposalDto.EmployeesAmount
            && p.CompanyNumber == proposalDto.CompanyNumber)), Times.Once);

        revenueServiceMock.Verify(r => r.GetRevenuePercent(proposalDto.CompanyNumber), Times.Once);
    }
}