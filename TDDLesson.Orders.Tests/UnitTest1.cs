using FluentAssertions;
using Moq;
using TDDLesson;

namespace TestProject1;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void ShouldApproveProposal_WhenAllConditionsMet()
    {
        // Arrange
        var proposal = new Proposal(new ProposalDto
        {
            CompanyNumber = 1,
            CompanyName = "Mindbox",
            CompanyEmail = "test@mindbox.cloud",
            EmployeesAmount = 101
        });

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
        var proposal = new Proposal(new ProposalDto
        {
            CompanyNumber = 1,
            CompanyName = "Mindbox",
            CompanyEmail = "test@mindbox.cloud",
            EmployeesAmount = 100
        });

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
        var proposal = new Proposal(new ProposalDto
        {
            CompanyNumber = 1,
            CompanyName = "Mindbox",
            CompanyEmail = "test@mindbox.cloud",
            EmployeesAmount = 101
        });

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
        var invitationalMessage = NotificationsHelper.BuildInvitationalMessage(proposal, processingDateTimeUtc);

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
        var invitationalMessage = NotificationsHelper.ShouldSentNotificationToForum(proposal, processingDateTimeUtc);

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
        var invitationalMessage = NotificationsHelper.ShouldSentNotificationToForum(proposal, processingDateTimeUtc);

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
        var invitationalMessage = NotificationsHelper.ShouldSentNotificationToForum(proposal, processingDateTimeUtc);

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
        
        
        var processor = new AccreditationProposalProcessor(
            revenueService,
            orderRepository,
            emailClient);
        
        //Act
        var task = processor.HandleProposal(proposalDto);
        
        //Assert
        emailClientMock.Verify(c => c.SendEmail("test@mindbox.cloud", "Invitation to forum", "<p>Hello!</p>"), Times.Once);
        emailClientMock.Verify(c => c.SendEmail("test@mindbox.cloud", "ProposalIsProcessed", "<p>Hello!</p>"), Times.Once);

        orderRepositoryMock.Verify(r => r.SaveAsync(It.Is<Proposal>(p =>
            p.CompanyName == proposalDto.CompanyName 
            && p.CompanyEmail == proposalDto.CompanyEmail
            && p.EmployeesAmount == proposalDto.EmployeesAmount
            && p.CompanyNumber == proposalDto.CompanyNumber)));
        
        revenueServiceMock.Verify(r => r.GetRevenuePercent(proposalDto.CompanyNumber), Times.Once);
    }
}