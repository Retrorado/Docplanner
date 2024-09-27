using Docplanner.Application.Doctors.Availability.Mappers;
using Docplanner.Application.Doctors.Availability.Services;
using Docplanner.Contracts.ExternalClients.AvailabilityClient.GetAvailability;
using Docplanner.Contracts.Queries.Doctors.Availability;
using Docplanner.Shared.Primitives;
using Docplanner.Tests.Shared.Factories.ExternalClients;
using FluentAssertions;

namespace Docplanner.Application.UnitTests.ExternalClients.Mappers.GetAvailabilityResponseMapperShouldTests;

public class MapToDtoShould
{
    private static DoctorWeeklyAvailabilityDto Act(GetAvailabilityResponse response, DateOnly startDate)
        => response.MapToDto(weekStartDate: startDate);

    private readonly DateOnly _startDate = new(year: 2024, month: 10, day: 1);

    [Fact]
    public void ReturnCorrectAvailabilityBasedOnWorkPeriod_WhenThereAlreadyAreBusySlots()
    {
        // Arrange
        const int slotDurationInMinutes = 30;
        const int startHour = 9;
        const int lunchStartHour = 11;
        const int lunchEndHour = 16;
        const int endHour = 17;

        var busySlots = new TimeSlot[]
        {
            new(Start: _startDate.ToDateTime(time: new TimeOnly(hour: 10, minute: 0)),
                End: _startDate.ToDateTime(time: new TimeOnly(hour: 10, minute: 30))),
            new(Start: _startDate.ToDateTime(time: new TimeOnly(hour: 10, minute: 30)),
                End: _startDate.ToDateTime(time: new TimeOnly(hour: 11, minute: 0)))
        };

        var response = GetAvailabilityResponsesFactory.Create(
            slotDurationMinutes: slotDurationInMinutes,
            monday: DaysFactory.Create(
                startHour: startHour,
                lunchStartHour: lunchStartHour,
                lunchEndHour: lunchEndHour,
                endHour: endHour,
                busySlots: busySlots)
        );

        // Act
        var result = Act(response: response, startDate: _startDate);

        // Assert
        var startDateTime = _startDate.ToDateTime(time: new TimeOnly(hour: startHour, minute: 0));
        var lunchStartDateTime = _startDate.ToDateTime(time: new TimeOnly(hour: lunchStartHour, minute: 0));
        var lunchEndDateTime = _startDate.ToDateTime(time: new TimeOnly(hour: lunchEndHour, minute: 0));
        var endDateTime = _startDate.ToDateTime(time: new TimeOnly(hour: endHour, minute: 0));

        var expectedMondaySlots = SlotGenerator.GetSlots(
                start: startDateTime,
                end: lunchStartDateTime,
                durationInMinutes: slotDurationInMinutes)
            .Concat(second: SlotGenerator.GetSlots(
                start: lunchEndDateTime,
                end: endDateTime,
                durationInMinutes: slotDurationInMinutes))
            .Except(second: busySlots).ToArray();

        result.Monday.Slots.Should().BeEquivalentTo(expectation: expectedMondaySlots);
        result.Facility.FacilityId.Should().Be(expected: response.Facility.FacilityId);
    }

    [Fact]
    public void ReturnCorrectAvailabilityBasedOnWorkPeriod_WhenThereAreNoBusySlots()
    {
        // Arrange
        const int slotDurationInMinutes = 30;
        const int startHour = 9;
        const int lunchStartHour = 11;
        const int lunchEndHour = 16;
        const int endHour = 17;

        var response = GetAvailabilityResponsesFactory.Create(
            slotDurationMinutes: slotDurationInMinutes,
            monday: DaysFactory.Create(
                startHour: startHour,
                lunchStartHour: lunchStartHour,
                lunchEndHour: lunchEndHour,
                endHour: endHour)
        );

        // Act
        var result = Act(response: response, startDate: _startDate);

        // Assert
        var startDateTime = _startDate.ToDateTime(time: new TimeOnly(hour: startHour, minute: 0));
        var lunchStartDateTime = _startDate.ToDateTime(time: new TimeOnly(hour: lunchStartHour, minute: 0));
        var lunchEndDateTime = _startDate.ToDateTime(time: new TimeOnly(hour: lunchEndHour, minute: 0));
        var endDateTime = _startDate.ToDateTime(time: new TimeOnly(hour: endHour, minute: 0));

        var expectedMondaySlots = SlotGenerator.GetSlots(
                start: startDateTime,
                end: lunchStartDateTime,
                durationInMinutes: slotDurationInMinutes)
            .Concat(second: SlotGenerator.GetSlots(
                start: lunchEndDateTime,
                end: endDateTime,
                durationInMinutes: slotDurationInMinutes))
            .ToArray();

        result.Monday.Slots.Should().BeEquivalentTo(expectation: expectedMondaySlots);
        result.Facility.FacilityId.Should().Be(expected: response.Facility.FacilityId);
    }

    [Fact]
    public void ReturnEmptySlots_WhenWorkPeriodIsNull()
    {
        // Arrange
        var response = GetAvailabilityResponsesFactory.Create(
            slotDurationMinutes: 30,
            monday: DaysFactory.CreateWithEmptyWorkPeriod()
        );

        // Act
        var result = Act(response: response, startDate: _startDate);

        // Assert
        result.Monday.Slots.Should().BeEmpty();
    }

    [Fact]
    public void ReturnEmptySlots_WhenBusySlotsCoverAllWorkPeriod()
    {
        // Arrange
        const int startHour = 9;
        const int lunchStartHour = 10;
        const int lunchEndHour = 11;
        const int endHour = 12;

        var busySlots = new TimeSlot[]
        {
            new(Start: _startDate.ToDateTime(time: new TimeOnly(hour: 9, minute: 0)),
                End: _startDate.ToDateTime(time: new TimeOnly(hour: 10, minute: 0))),
            new(Start: _startDate.ToDateTime(time: new TimeOnly(hour: 11, minute: 0)),
                End: _startDate.ToDateTime(time: new TimeOnly(hour: 12, minute: 0)))
        };

        var response = GetAvailabilityResponsesFactory.Create(
            slotDurationMinutes: 60,
            facilityId: Guid.NewGuid(),
            monday: DaysFactory.Create(
                startHour: startHour,
                lunchStartHour: lunchStartHour,
                lunchEndHour: lunchEndHour,
                endHour: endHour,
                busySlots: busySlots)
        );

        // Act
        var result = Act(response: response, startDate: _startDate);

        // Assert
        result.Monday.Slots.Should().BeEmpty();
    }
}