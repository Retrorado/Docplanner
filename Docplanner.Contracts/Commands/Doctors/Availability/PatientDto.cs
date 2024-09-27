using FluentValidation;

namespace Docplanner.Contracts.Commands.Doctors.Availability;

public class PatientDto
{
    public string Name { get; init; } = null!;
    public string SecondName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Phone { get; init; } = null!;
}

public class PatientDtoValidator : AbstractValidator<PatientDto>
{
    public PatientDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.SecondName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Phone).NotEmpty();
    }
}