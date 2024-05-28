using FluentAssertions;
using TDDLesson;

namespace TestProject1;

[TestClass]
public sealed class StatusEvaluatorTests
{
    private static readonly DateTime UtcNow = DateTime.UtcNow;

    [TestMethod]
    [DataRow(100, 0.30f)]
    [DataRow(101, 0.30f)]
    [DataRow(99, 0.31f)]
    public void Evaluate_CompanyDoesNotSatisfyRequirements_Declined(int employeesAmount, float revenuePercent)
    {
        // Arrange
        var dto = CreateDto(employeesAmount);

        // Act
        var status = StatusEvaluator.Evaluate(dto, UtcNow, revenuePercent);

        // Assert
        status.Should().Be(ProposalStatus.Declined);
    }
    
    [TestMethod]
    [DataRow(500)]
    [DataRow(101)]
    public void Evaluate_CompanyEmployeesAmountLessThan500_Processed(int employeesAmount)
    {
        // Arrange
        var dto = CreateDto(employeesAmount);

        // Act
        var status = StatusEvaluator.Evaluate(dto, UtcNow, 0.5f);

        // Assert
        status.Should().Be(ProposalStatus.Processed);
    }
    
    [TestMethod]
    [DataRow("2024-06-01")]
    [DataRow("2024-07-12")]
    [DataRow("2024-08-31")]
    public void Evaluate_CompanyEmployeesAmountLessThan500_DateSatisfyRange_Processed(string dateString)
    {
        // Arrange
        var dto = CreateDto(400);
        var date = DateTime.Parse(dateString);

        // Act
        var status = StatusEvaluator.Evaluate(dto, date, 0.5f);

        // Assert
        status.Should().Be(ProposalStatus.Processed);
    }
    
    [TestMethod]
    [DataRow("2024-05-31")]
    [DataRow("2024-09-01")]
    public void Evaluate_CompanyEmployeesAmountMoreThan500_DateDoesNotSatisfyRange_Processed(string dateString)
    {
        // Arrange
        var dto = CreateDto(501);
        var date = DateTime.Parse(dateString);

        // Act
        var status = StatusEvaluator.Evaluate(dto, date, 0.5f);

        // Assert
        status.Should().Be(ProposalStatus.Processed);
    }
    
    [TestMethod]
    [DataRow("2024-06-01")]
    [DataRow("2024-07-12")]
    [DataRow("2024-08-31")]
    public void Evaluate_CompanyEmployeesAmountMoreThan500_DateSatisfiesRange_ProcessedAndInvited(string dateString)
    {
        // Arrange
        var dto = CreateDto(501);
        var date = DateTime.Parse(dateString);

        // Act
        var status = StatusEvaluator.Evaluate(dto, date, 0.5f);

        // Assert
        status.Should().Be(ProposalStatus.ProcessedAndInvited);
    }

    private static ProposalDto CreateDto(int employeesAmount)
    {
        return new ProposalDto
        {
            EmployeesAmount = employeesAmount,
            CompanyNumber = 42,
            CompanyName = "Mindbox",
            CompanyEmail = "mindbox@mindbox.cloud"
        };
    }
}