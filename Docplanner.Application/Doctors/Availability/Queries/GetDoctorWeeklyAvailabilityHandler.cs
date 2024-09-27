using Docplanner.Application.Doctors.Availability.Mappers;
using Docplanner.Application.ExternalClients;
using Docplanner.Contracts.Queries.Doctors.Availability;
using MediatR;

namespace Docplanner.Application.Doctors.Availability.Queries;

public class GetDoctorWeeklyAvailabilityHandler(IAvailabilityClient availabilityClient)
    : IRequestHandler<GetDoctorWeeklyAvailability, DoctorWeeklyAvailabilityDto>
{
    public async Task<DoctorWeeklyAvailabilityDto> Handle(GetDoctorWeeklyAvailability request, CancellationToken cancellationToken)
    {
        var doctorAvailabilities = await availabilityClient.GetAvailabilitiesAsync(request.WeekStartDate, cancellationToken);
        return doctorAvailabilities.MapToDto(DateOnly.FromDateTime(request.WeekStartDate));
    }
}