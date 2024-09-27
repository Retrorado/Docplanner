using Docplanner.Application.Doctors.Availability.Services;
using Docplanner.Contracts.ExternalClients.AvailabilityClient.GetAvailability;
using Docplanner.Contracts.Queries.Doctors.Availability;
using Docplanner.Shared.Primitives;
using Day = Docplanner.Contracts.Queries.Doctors.Availability.Day;
using FacilityDto = Docplanner.Contracts.Queries.Doctors.Availability.FacilityDto;

namespace Docplanner.Application.Doctors.Availability.Mappers;

public static class GetAvailabilityResponseMapper
{
    public static DoctorWeeklyAvailabilityDto MapToDto(this GetAvailabilityResponse response, DateOnly weekStartDate)
    {
        return new DoctorWeeklyAvailabilityDto
        {
            Monday = MapDay(response.Monday, response.SlotDurationMinutes, weekStartDate),
            Tuesday = MapDay(response.Tuesday, response.SlotDurationMinutes, weekStartDate.AddDays(1)),
            Wednesday = MapDay(response.Wednesday, response.SlotDurationMinutes, weekStartDate.AddDays(2)),
            Thursday = MapDay(response.Thursday, response.SlotDurationMinutes, weekStartDate.AddDays(3)),
            Friday = MapDay(response.Friday, response.SlotDurationMinutes, weekStartDate.AddDays(4)),
            Saturday = MapDay(response.Saturday, response.SlotDurationMinutes, weekStartDate.AddDays(5)),
            Sunday = MapDay(response.Sunday, response.SlotDurationMinutes, weekStartDate.AddDays(6)),
            Facility = new FacilityDto { FacilityId = response.Facility.FacilityId }
        };
    }

    private static Day MapDay(Contracts.ExternalClients.AvailabilityClient.GetAvailability.Day? sourceDay, int slotDurationMinutes, DateOnly date)
    {
        var allSlots = GetAllSlots(sourceDay, slotDurationMinutes, date);

        var availableSlots = allSlots.Except(sourceDay?.BusySlots ?? []).ToArray();

        return new Day
        {
            Slots = availableSlots.ToArray()
        };
    }

    private static TimeSlot[] GetAllSlots(
        Contracts.ExternalClients.AvailabilityClient.GetAvailability.Day? sourceDay,
        int slotDurationMinutes,
        DateOnly date)
    {
        if (sourceDay?.WorkPeriod == null)
        {
            return [];
        }

        var slots = new List<TimeSlot>();
        var workPeriod = sourceDay.WorkPeriod;

        slots.AddRange(SlotGenerator.GetSlots(
            date.ToDateTime(new TimeOnly(workPeriod.StartHour, 0)),
            date.ToDateTime(new TimeOnly(workPeriod.LunchStartHour, 0)),
            slotDurationMinutes));

        slots.AddRange(SlotGenerator.GetSlots(
            date.ToDateTime(new TimeOnly(workPeriod.LunchEndHour, 0)),
            date.ToDateTime(new TimeOnly(workPeriod.EndHour, 0)),
            slotDurationMinutes));

        return slots.ToArray();
    }
}