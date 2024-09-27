using Docplanner.Shared.Primitives;

namespace Docplanner.Contracts.ExternalClients.AvailabilityClient.GetAvailability;

public class Day
{
    public WorkPeriod? WorkPeriod { get; set; }
    public TimeSlot[]? BusySlots { get; set; }
}