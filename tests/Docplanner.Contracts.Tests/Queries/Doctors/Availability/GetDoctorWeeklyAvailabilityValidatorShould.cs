using Docplanner.Contracts.Queries.Doctors.Availability;
using FluentValidation.TestHelper;

namespace Docplanner.Contracts.Tests.Queries.Doctors.Availability;

public class GetDoctorWeeklyAvailabilityValidatorShould
{
    private readonly GetDoctorWeeklyAvailabilityValidator _validator = new();

    [Fact]
    public void ValidateWeekStartDateIsValid()
    {
        // Arrange
        var request = new GetDoctorWeeklyAvailability(new DateTime(2023, 10, 2)); // A Monday

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.WeekStartDate);
    }
    
    [Fact]
    public void ValidateWeekStartDateIsNotEmpty()
    {
        // Arrange
        var request = new GetDoctorWeeklyAvailability(default);

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.WeekStartDate);
    }

    [Fact]
    public void ValidateWeekStartDateIsMonday()
    {
        // Arrange
        var request = new GetDoctorWeeklyAvailability(new DateTime(2023, 10, 3)); // Not a Monday

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.WeekStartDate);
    }
}