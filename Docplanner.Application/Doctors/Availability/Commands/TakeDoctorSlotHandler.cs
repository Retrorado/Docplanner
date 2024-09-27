using Docplanner.Application.Doctors.Availability.Mappers;
using Docplanner.Application.ExternalClients;
using Docplanner.Contracts.Commands.Doctors.Availability;
using MediatR;

namespace Docplanner.Application.Doctors.Availability.Commands;

public class TakeDoctorSlotHandler(IAvailabilityClient availabilityClient) : IRequestHandler<TakeDoctorSlot>
{
    public async Task Handle(TakeDoctorSlot request, CancellationToken cancellationToken)
    {
        var takeSlotRequest = TakeSlotRequestMapper.FromCommand(request);
        await availabilityClient.TakeSlotAsync(takeSlotRequest, cancellationToken);
    }
}