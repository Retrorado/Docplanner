using FluentValidation;
using MediatR;

namespace Docplanner.Contracts.Queries.Doctors.Availability;

public class GetDoctorWeeklyAvailability : IRequest<DoctorWeeklyAvailabilityDto>
{
    public DateTime WeekStartDate { get; }

    public GetDoctorWeeklyAvailability(DateTime weekStartDate)
    {
        WeekStartDate = weekStartDate;
    }
}

public class GetDoctorWeeklyAvailabilityValidator : AbstractValidator<GetDoctorWeeklyAvailability>
{
    public GetDoctorWeeklyAvailabilityValidator()
    {
        RuleFor(x => x.WeekStartDate).NotEmpty();
        RuleFor(x => x.WeekStartDate).Must(x => x.DayOfWeek == DayOfWeek.Monday);
    }
}