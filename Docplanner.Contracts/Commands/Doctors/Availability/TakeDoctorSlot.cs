using FluentValidation;
using MediatR;

namespace Docplanner.Contracts.Commands.Doctors.Availability;

public class TakeDoctorSlot : IRequest
{
    public DateTime Start { get; init; }
    public DateTime End { get; init; }
    public string Comment { get; init; } = null!;
    public PatientDto Patient { get; init; } = null!;
    public Guid FacilityId { get; init; }
}

public class TakeDoctorSlotValidator : AbstractValidator<TakeDoctorSlot>
{
    public TakeDoctorSlotValidator()
    {
        RuleFor(x => x.Start).NotEmpty();
        RuleFor(x => x.End).NotEmpty();
        RuleFor(x => x.Comment).NotEmpty();
        RuleFor(x => x.Patient).NotEmpty().SetValidator(new PatientDtoValidator());
        RuleFor(x => x.FacilityId).NotEmpty();
    }
}