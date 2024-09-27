using Docplanner.Shared.Primitives;

namespace Docplanner.Application.Doctors.Availability.Services;

public static class SlotGenerator
{
    public static TimeSlot[] GetSlots(DateTime start, DateTime end, int durationInMinutes)
    {
        if (durationInMinutes <= 0)
        {
            return [];
        }
        
        var slots = new List<TimeSlot>();
        var currentStart = start;

        while (currentStart < end)
        {
            var slotEnd = currentStart.AddMinutes(durationInMinutes);
            if (slotEnd > end)
            {
                break;
            }

            slots.Add(new TimeSlot(currentStart, slotEnd));

            currentStart = slotEnd;
        }

        return slots.ToArray();
    }
}