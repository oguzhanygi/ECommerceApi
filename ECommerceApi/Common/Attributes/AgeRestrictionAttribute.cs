using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Common.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class AgeRestrictionAttribute(int minAge, int maxAge) : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext context)
    {
        // Type safety
        if (value is not null and not DateTime)
            throw new InvalidOperationException(
                $"{nameof(AgeRestrictionAttribute)} can only be applied to DateTime properties. " +
                $"Invalid usage on: {context.MemberName} ({value.GetType().Name})");

        // Null handling (explicit)
        if (value is null)
            return new ValidationResult("Birth date is required.");

        // Age calculation
        var dateOfBirth = (DateTime)value;
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;
        
        if (dateOfBirth.Date > today.AddYears(-age))
            age--;

        // Validation
        if (age < minAge)
            return new ValidationResult($"Must be at least {minAge} years old. Current age: {age}.");
            
        if (age > maxAge)
            return new ValidationResult($"Maximum allowed age is {maxAge}. Current age: {age}.");

        return ValidationResult.Success;
    }
}