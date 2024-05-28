using TDDLesson;
using TDDLesson.Responses;

namespace TDDLesson.Orders.Tests;

[TestClass]
public class ValidationServiceTests
{
    [TestMethod]
    public void Validate_EmptyCompanyName_ReturnsInvalidStatusAndMessage()
    {
        // Arrange
        var proposalDto = CreateProposalDto(companyName: "");
        
        // Act
        var result = ValidationService.Validate(proposalDto);

        // Assert
        Assert.AreEqual(ValidationService.EmptyCompanyNameMessage, result.Message);
        Assert.AreEqual(ValidationStatus.Invalid, result.ValidationStatus);
    }

    [TestMethod]
    public void Validate_EmptyCompanyEmail_ReturnsInvalidStatusAndMessage()
    {
        // Arrange
        var proposalDto = CreateProposalDto(companyEmail: "");
        
        // Act
        var result = ValidationService.Validate(proposalDto);

        // Assert
        Assert.AreEqual(ValidationService.EmptyCompanyEmailMessage, result.Message);
        Assert.AreEqual(ValidationStatus.Invalid, result.ValidationStatus);
    }

    [TestMethod]
    public void Validate_CompanyEmailIsNotValid_ReturnsInvalidStatusAndMessage()
    {
        // Arrange
        var proposalDto = CreateProposalDto(companyEmail: "invalid-email");
        
        // Act
        var result = ValidationService.Validate(proposalDto);

        // Assert
        Assert.AreEqual(ValidationService.CompanyEmailIsNotValidMessage, result.Message);
        Assert.AreEqual(ValidationStatus.Invalid, result.ValidationStatus);
    }

    [TestMethod]
    public void Validate_DataIsCorrect_ReturnsValidStatusWithoutMessage()
    {
        // Arrange
        var proposalDto = CreateProposalDto();

        // Act
        var result = ValidationService.Validate(proposalDto);
        
        // Assert
        Assert.IsNull(result.Message);
        Assert.AreEqual(ValidationStatus.Valid, result.ValidationStatus);
    }

    private static ProposalDto CreateProposalDto(
        int companyNumber = 1,
        string companyName = "Test Company",
        string companyEmail = "test@example.com",
        int employeesAmount = 101)
    {
        return new ProposalDto
        {
            CompanyNumber = companyNumber,
            CompanyName = companyName,
            CompanyEmail = companyEmail,
            EmployeesAmount = employeesAmount
        };
    }
}