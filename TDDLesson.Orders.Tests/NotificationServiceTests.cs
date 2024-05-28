using System.Runtime.InteropServices.JavaScript;
using FluentAssertions;
using TDDLesson;

namespace TestProject1;

[TestClass]
public class NotificationServiceTests
{
    [TestMethod]
    public void ShouldReturnNull_WhenEmployeesNotSaved()
    {
        var notificationRequest = new NotificationRequest(
            false,
            99, 
            new DateTime(2024, 07, 01), 
            "123@gmail.com");
        
        var emailDto = NotificationService.CreateNotification(notificationRequest);
        
        emailDto.Should().BeNull();
    }
    
    [TestMethod]
    public void ShouldReturnNull_WhenDataNotInInterval()
    {
        var notificationRequest = new NotificationRequest(
            true,
            101, 
            new DateTime(1990, 07, 01), 
            "123@gmail.com");
        
        var emailDto = NotificationService.CreateNotification(notificationRequest);
        
        emailDto.Should().BeNull();
    }

    [TestMethod]
    public void ShouldReturnEmailDto_WhenCorrectData()
    {
        var notificationRequest = new NotificationRequest(
            true,
            101, 
            new DateTime(2024, 07, 01), 
            "123@gmail.com");
        
        var emailDto = NotificationService.CreateNotification(notificationRequest);
        
        emailDto.Should().NotBeNull();
    }
    
    [TestMethod]
    public void ShouldReturnEmailDtoWithoutInvite_WhenCorrectDataAndLess500Employee()
    {
        var notificationRequest = new NotificationRequest(
            true,
            150, 
            new DateTime(2024, 07, 01), 
            "123@gmail.com");
        
        var emailDto = NotificationService.CreateNotification(notificationRequest);
        
        emailDto.Should().NotBeNull();
        emailDto.MailTo.Should().Be("123@gmail.com");
        emailDto.Subject.Should().Be(NotificationService.Subject);
        emailDto.Body.Should().Be(NotificationService.ProcessedMessage);

    }
    
    [TestMethod]
    public void ShouldReturnEmailDtoWithInvite_WhenCorrectDataAndMore500Employee()
    {
        var notificationRequest = new NotificationRequest(
            true,
            550, 
            new DateTime(2024, 07, 01), 
            "123@gmail.com");
        
        var emailDto = NotificationService.CreateNotification(notificationRequest);
        
        emailDto.Should().NotBeNull();
        emailDto.MailTo.Should().Be("123@gmail.com");
        emailDto.Subject.Should().Be(NotificationService.Subject);
        emailDto.Body.Should().Be(NotificationService.ProcessedMessage + " " + NotificationService.InviteMessage);

    }
}