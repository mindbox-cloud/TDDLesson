using FluentAssertions;
using TDDLesson.Responses;

namespace TDDLesson.Orders.Tests;

[TestClass]
public class MessageMapperTests
{
    [DataRow(ProposalStatus.Declined, Messages.DeclinedSubject, Messages.DeclinedBodyTemplate)]
    [DataRow(ProposalStatus.Processed, Messages.ProcessedSubject, Messages.ProcessedBodyTemplate)]
    [DataRow(ProposalStatus.ProcessedAndInvited, Messages.InviteSubject, Messages.InviteBodyTemplate)]
    [TestMethod]
    public void GetMessage_ValidStatus_ReturnsSubjectAndBody(ProposalStatus status, string exceptedSubject, string exceptedBody)
    {
        // Act
        var (actualSubject, actualBody) = MessageMapper.MapMessage(status);
        
        // Assert
        actualSubject.Should().Be(exceptedSubject);
        actualBody.Should().Be(exceptedBody);
    }
}