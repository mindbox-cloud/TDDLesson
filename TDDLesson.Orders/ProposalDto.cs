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
    private const int MinEmployeesAmount = 100;
    private const float MinITRevenuePercent = 30;

    public static bool IsAppropriate(
        this ProposalDto proposalDto,
        float revenuePercent)
    {
        if (!IsValid(proposalDto))
            return false;
        return proposalDto.EmployeesAmount > MinEmployeesAmount && revenuePercent > MinITRevenuePercent;
    }

    private static bool IsValid(ProposalDto proposalDto)
    {
        if (proposalDto.EmployeesAmount < 0 || proposalDto.CompanyNumber < 0)
            throw new ArgumentException();

        return true;
    }
}