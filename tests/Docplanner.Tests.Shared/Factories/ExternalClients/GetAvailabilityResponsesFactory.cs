using Docplanner.Contracts.ExternalClients.AvailabilityClient.GetAvailability;

namespace Docplanner.Tests.Shared.Factories.ExternalClients;

public static class GetAvailabilityResponsesFactory
{
    public static GetAvailabilityResponse Create(
        int slotDurationMinutes = 30,
        Guid? facilityId = null,
        Day? monday = null)
    {
        return new GetAvailabilityResponse
        {
            Monday = monday,
            SlotDurationMinutes = slotDurationMinutes,
            Facility = new FacilityDto { FacilityId = facilityId ?? Guid.NewGuid() }
        };
    }
}