using System.Text.RegularExpressions;

namespace TDDLesson;

public static class ValidateService
{
    public const string EmptyCompanyNameMessage = "Company name is empty";
    public const string EmptyCompanyEmailMessage = "Company email is empty";
    public const string CompanyEmailIsNotValidMessage = "Company email is not valid";
    
    public static ValidationResult Validate(ProposalDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.CompanyName))
        {
            return new ValidationResult(ValidationStatus.Invalid)
            {
                Message = EmptyCompanyNameMessage
            };
        }
        
        if (string.IsNullOrWhiteSpace(dto.CompanyEmail))
        {
            return new ValidationResult(ValidationStatus.Invalid)
            {
               Message = EmptyCompanyEmailMessage
            };
        }

        if (!IsValidEmail(dto.CompanyEmail))
        {
            return new ValidationResult(ValidationStatus.Invalid)
            {
                Message = CompanyEmailIsNotValidMessage
            };
        }
        
        return new ValidationResult(ValidationStatus.Valid);
    }

    private static bool IsValidEmail(string email)
    {
        const string pattern = @"([a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\.[a-zA-Z0-9_-]+)";
        var regex = new Regex(pattern);
        return regex.IsMatch(email);
    }
}

