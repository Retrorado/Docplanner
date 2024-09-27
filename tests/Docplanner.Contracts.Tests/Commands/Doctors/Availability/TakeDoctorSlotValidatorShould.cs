using Docplanner.Contracts.Commands.Doctors.Availability;
using Docplanner.Tests.Shared.Factories.Commands;
using FluentValidation.TestHelper;

namespace Docplanner.Contracts.Tests.Commands.Doctors.Availability;

public class TakeDoctorSlotValidatorShould
{
    private readonly TakeDoctorSlotValidator _validator = new();

    [Fact]
    public void ValidateValidCommand()
    {
        // Arrange
        var command = TakeDoctorSlotCommandsFactory.Create();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void ValidateStartIsNotEmpty()
    {
        // Arrange
        var command = TakeDoctorSlotCommandsFactory.Create(start: DateTime.MinValue);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Start);
    }

    [Fact]
    public void ValidateEndIsNotEmpty()
    {
        // Arrange
        var command = TakeDoctorSlotCommandsFactory.Create(end: DateTime.MinValue);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.End);
    }

    [Fact]
    public void ValidateCommentIsNotEmpty()
    {
        // Arrange
        var command = TakeDoctorSlotCommandsFactory.Create(comment: "");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Comment);
    }

    [Fact]
    public void ValidatePatientIsNotEmpty()
    {
        // Arrange
        var command = TakeDoctorSlotCommandsFactory.CreateWithEmptyPatient();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Patient);
    }

    [Fact]
    public void ValidateFacilityIdIsNotEmpty()
    {
        // Arrange
        var command = TakeDoctorSlotCommandsFactory.Create(facilityId: Guid.Empty);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FacilityId);
    }

    [Fact]
    public void ValidatePatientFields()
    {
        // Arrange
        var invalidPatient = new PatientDto
        {
            Name = "",
            SecondName = "",
            Email = "invalid-email",
            Phone = ""
        };
        var command = TakeDoctorSlotCommandsFactory.Create(patient: invalidPatient);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor("Patient.Name");
        result.ShouldHaveValidationErrorFor("Patient.SecondName");
        result.ShouldHaveValidationErrorFor("Patient.Email");
        result.ShouldHaveValidationErrorFor("Patient.Phone");
    }
}