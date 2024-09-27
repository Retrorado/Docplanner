using Docplanner.Contracts.ExternalClients.AvailabilityClient.GetAvailability;
using Docplanner.Shared.Primitives;

namespace Docplanner.Tests.Shared.Factories.ExternalClients;

public static class DaysFactory
{
    public static Day Create(
        int startHour = 9,
        int lunchStartHour = 11,
        int lunchEndHour = 16,
        int endHour = 17,
        TimeSlot[]? busySlots = null)
    {
        return new Day
        {
            WorkPeriod = new WorkPeriod
            {
                StartHour = startHour,
                LunchStartHour = lunchStartHour,
                LunchEndHour = lunchEndHour,
                EndHour = endHour
            },
            BusySlots = busySlots?.ToArray() ?? []
        };
    }

    public static Day CreateWithEmptyWorkPeriod(TimeSlot[]? busySlots = null)
    {
        return new Day
        {
            WorkPeriod = null,
            BusySlots = busySlots?.ToArray() ?? []
        };
    }
}