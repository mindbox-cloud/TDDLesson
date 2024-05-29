using FluentAssertions;

namespace TDDLesson;

[TestClass]
public class EmailTests
{
    [TestMethod]
    [DataRow("2024-06-01")]
    [DataRow("2024-09-01")]
    [DataRow("2024-08-05")]
    public void GetEmailsForProposal_ProposalDateInRange_EmailWithoutInvitationCreated(string dateTime)
    {
        var validDate = DateTime.Parse(dateTime);
        var proposal = Proposal.Create(1,"TestCompany",  0.31f, 200, "test@email.ru");
        var expected = new List<Email>
        {
            new (proposal.CompanyEmail, Constants.SuccessOrderProcessingSubject, Constants.SuccessOrderProcessingBody)
        };
        
        var emails = EmailService.GetEmailsForProposal(proposal, validDate);

        emails.Should().HaveCount(1);
        emails.Should().BeEquivalentTo(expected);
    }

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
    [DataRow("2024-05-31")]
    [DataRow("2024-09-02")]
    [DataRow("2023-08-05")]
    public void GetEmailsForProposal_ProposalDateOutOfRange_EmailNotCreated(string dateTime)
    {
        var validDate = DateTime.Parse(dateTime);
        var proposal = Proposal.Create(1,"TestCompany",  0.31f, 501, "test@email.ru");
        
        var emails = EmailService.GetEmailsForProposal(proposal, validDate);

        emails.Should().BeEmpty();
    }
}