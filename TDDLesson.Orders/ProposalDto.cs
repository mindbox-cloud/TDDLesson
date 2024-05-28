namespace TDDLesson;

public sealed record ProposalDto
{
    public required int CompanyNumber { get; init; }
    
    public required string CompanyName { get; init; }
    
    public required string CompanyEmail { get; init; }
    
    public required int EmployeesAmount { get; init; }
}

public static class ProposalDtoExtensions
{
    public static bool IsAppropriate(
        this ProposalDto proposalDto,
        float revenuePercent)
    {
        if (proposalDto.EmployeesAmount <= 100) return false; 
        return true;
    }
}