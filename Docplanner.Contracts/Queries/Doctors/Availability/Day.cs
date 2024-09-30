using Docplanner.Shared.Primitives;

namespace Docplanner.Contracts.Queries.Doctors.Availability;

public class Day
{
    public TimeSlot[] Slots { get; init; } = [];
}