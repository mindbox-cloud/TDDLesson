namespace TDDLesson;

public static class EmailService
{
    private static DateTime MinDate => DateTime.Parse(Constants.MinNotificationDate);
    private static DateTime MaxDate => DateTime.Parse(Constants.MaxNotificationDate);

    public static List<Email> GetEmailsForProposal(Proposal proposal, DateTime dateTime)
    {
        var emails = new List<Email>();
        if (dateTime >= MinDate && dateTime <= MaxDate)
        {
            emails.Add(new Email(proposal.CompanyEmail, Constants.SuccessOrderProcessingSubject,
                Constants.SuccessOrderProcessingBody));

            var invitation = CreateInvitation(proposal);
            if (invitation is not null)
                emails.Add(invitation);
        }

        return emails;
    }

    private static Email? CreateInvitation(Proposal proposal)
    {
        if (proposal.EmployeesAmount <= Constants.EmployeesAmountForInvitation)
            return null;
            
        return new Email(proposal.CompanyEmail, Constants.ForumInvitationSubject,
                $"{proposal.CompanyName}! " + Constants.ForumInvitationBody);
    }
}