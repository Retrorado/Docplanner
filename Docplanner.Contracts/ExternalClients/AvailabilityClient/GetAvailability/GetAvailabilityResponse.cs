namespace Docplanner.Contracts.ExternalClients.AvailabilityClient.GetAvailability;

public class GetAvailabilityResponse
{
    public int SlotDurationMinutes { get; init; }
    public Day? Monday { get; init; }
    public Day? Tuesday { get; init; }
    public Day? Wednesday { get; init; }
    public Day? Thursday { get; init; }
    public Day? Friday { get; init; }
    public Day? Saturday { get; init; }
    public Day? Sunday { get; init; }
    public FacilityDto Facility { get; init; } = null!;
}