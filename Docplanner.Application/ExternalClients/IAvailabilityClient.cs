using Docplanner.Contracts.ExternalClients.AvailabilityClient.GetAvailability;
using Docplanner.Contracts.ExternalClients.AvailabilityClient.TakeSlot;

namespace Docplanner.Application.ExternalClients;

public interface IAvailabilityClient
{
    Task<GetAvailabilityResponse> GetAvailabilitiesAsync(DateTime startDate, CancellationToken cancellationToken);
    Task TakeSlotAsync(TakeSlotRequest request, CancellationToken cancellationToken);
}