using Docplanner.Shared.Primitives;

namespace Docplanner.Contracts.ExternalClients.AvailabilityClient.GetAvailability;

public class Day
{
    public WorkPeriod? WorkPeriod { get; init; } 
    public TimeSlot[]? BusySlots { get; init; }
}