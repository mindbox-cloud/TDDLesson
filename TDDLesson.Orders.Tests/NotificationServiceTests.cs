using System.Runtime.InteropServices.JavaScript;
using FluentAssertions;
using TDDLesson;

namespace TestProject1;

[TestClass]
public class NotificationServiceTests
{
    [TestMethod]
    [DataRow(100)]
    [DataRow(50)]
    public void ShouldReturnNull_WhenEmployeesNumberLessThan100(int employeeAmount)
    {
        var notificationRequest = new NotificationRequest(employeeAmount, new DateTime(2024, 07, 01), "123@gmail.com");
        
        
        var emailDto = NotificationService.CreateNotification(notificationRequest);


        emailDto.Should().BeNull();
    }
    
    [TestMethod]
    public void ShouldReturnNull_WhenDataNotInInterval()
    {
        var notificationRequest = new NotificationRequest(101, new DateTime(1990, 07, 01), "123@gmail.com");
        
        
        var emailDto = NotificationService.CreateNotification(notificationRequest);


        emailDto.Should().BeNull();
    }

    [TestMethod]
    public void ShouldReturnEmailDto_WhenCorrectData()
    {
        var notificationRequest = new NotificationRequest(101, new DateTime(2024, 07, 01), "123@gmail.com");
        
        
        var emailDto = NotificationService.CreateNotification(notificationRequest);


        emailDto.Should().NotBeNull();
    }
}