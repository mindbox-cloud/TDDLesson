using FluentAssertions;

namespace TDDLesson;

[TestClass]
public class EmailTests
{
    [TestMethod]
    [DataRow("2024-06-01")]
    [DataRow("2024-09-01")]
    [DataRow("2024-08-05")]
    public void GetEmailsForProposal_CompanyMeetsRequirements_EmailWithInvitationCreated(string dateTime)
    {
        var validDate = DateTime.Parse(dateTime);
        var proposal = Proposal.Create(1,"TestCompany",  0.31f, 501, "test@email.ru");
        var expected = new List<Email>
        {
            new (proposal.CompanyEmail, Constants.SuccessOrderProcessingSubject, Constants.SuccessOrderProcessingBody),
            new (proposal.CompanyEmail, Constants.ForumInvitationSubject, $"{proposal.CompanyName}! " + Constants.ForumInvitationBody)
        };
        
        var emails = EmailService.GetEmailsForProposal(proposal, validDate);

        emails.Should().HaveCount(2);
        emails.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    [DataRow("2024-06-01")]
    [DataRow("2024-09-01")]
    [DataRow("2024-08-05")]
    public void ValidateNotification_DateTimeInRange_Success(string dateTime)
    {
        var validDate = DateTime.Parse(dateTime);
        var shouldNotify = AccreditationProposalProcessor.ValidateNotification(validDate);

        shouldNotify.Should().BeTrue();
    }
    
    [TestMethod]
    [DataRow("2024-05-31")]
    [DataRow("2024-09-02")]
    [DataRow("2023-08-05")]
    public void ValidateNotification_DateTimeOutOfRange_Failed(string dateTime)
    {
        var validDate = DateTime.Parse(dateTime);
        var shouldNotify = AccreditationProposalProcessor.ValidateNotification(validDate);

        shouldNotify.Should().BeFalse();
    }

    [TestMethod]
    [DataRow(501)]
    [DataRow(1000)]
    public void ShouldSentInvitation_EnoughEmployeeCount_True(int employeeCount)
    {
        var shouldSendInvitation = AccreditationProposalProcessor.ShouldSendInvitation(employeeCount);

        shouldSendInvitation.Should().BeTrue();
    }
    
    [TestMethod]
    [DataRow(500)]
    [DataRow(200)]
    [DataRow(101)]
    public void ShouldSentInvitation_NotEnoughEmployeeCount_False(int employeeCount)
    {
        var shouldSendInvitation = AccreditationProposalProcessor.ShouldSendInvitation(employeeCount);

        shouldSendInvitation.Should().BeFalse();
    }
}