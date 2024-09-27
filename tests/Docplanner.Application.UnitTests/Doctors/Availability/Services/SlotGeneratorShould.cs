using Docplanner.Application.Doctors.Availability.Services;
using Docplanner.Shared.Primitives;
using FluentAssertions;

namespace Docplanner.Application.UnitTests.Doctors.Availability.Services;

public class SlotGeneratorShould
{
    private static TimeSlot[] Act(DateTime start, DateTime end, int durationInMinutes)
        => SlotGenerator.GetSlots(start, end, durationInMinutes);

    [Fact]
    public void GenerateSlotsWithinGivenTimeRange()
    {
        // Arrange
        var start = new DateTime(2024, 10, 1, 9, 0, 0);
        var end = new DateTime(2024, 10, 1, 11, 0, 0);
        const int durationInMinutes = 30;

        // Act
        var slots = Act(start, end, durationInMinutes);

        // Assert
        slots.Should().HaveCount(4);
        slots[0].Start.Should().Be(start);
        slots[0].End.Should().Be(start.AddMinutes(durationInMinutes));
        slots[1].Start.Should().Be(start.AddMinutes(durationInMinutes));
        slots[1].End.Should().Be(start.AddMinutes(2 * durationInMinutes));
        slots[2].Start.Should().Be(start.AddMinutes(2 * durationInMinutes));
        slots[2].End.Should().Be(start.AddMinutes(3 * durationInMinutes));
        slots[3].Start.Should().Be(start.AddMinutes(3 * durationInMinutes));
        slots[3].End.Should().Be(end);
    }

    [Fact]
    public void GenerateSlotsForTimeRangeNotFullyDivisibleByDuration()
    {
        // Arrange
        var start = new DateTime(2024, 10, 1, 9, 0, 0);
        var end = new DateTime(2024, 10, 1, 10, 45, 0);
        const int durationInMinutes = 30;

        // Act
        var slots = Act(start, end, durationInMinutes);

        // Assert
        slots.Should().HaveCount(3);
        slots[0].Start.Should().Be(start);
        slots[0].End.Should().Be(start.AddMinutes(durationInMinutes));
        slots[1].Start.Should().Be(start.AddMinutes(durationInMinutes));
        slots[1].End.Should().Be(start.AddMinutes(2 * durationInMinutes));
        slots[2].Start.Should().Be(start.AddMinutes(2 * durationInMinutes));
        slots[2].End.Should().Be(start.AddMinutes(3 * durationInMinutes));
    }

    [Fact]
    public void ReturnEmptyArrayWhenTimeRangeIsShorterThanDuration()
    {
        // Arrange
        var start = new DateTime(2024, 10, 1, 9, 0, 0);
        var end = new DateTime(2024, 10, 1, 9, 15, 0);
        const int durationInMinutes = 30;

        // Act
        var slots = Act(start, end, durationInMinutes);

        // Assert
        slots.Should().BeEmpty();
    }

    [Fact]
    public void ReturnEmptyArrayWhenStartIsAfterEnd()
    {
        // Arrange
        var start = new DateTime(2024, 10, 1, 12, 0, 0);
        var end = new DateTime(2024, 10, 1, 9, 0, 0);
        const int durationInMinutes = 30;

        // Act
        var slots = Act(start, end, durationInMinutes);

        // Assert
        slots.Should().BeEmpty();
    }

    [Fact]
    public void ReturnEmptyArrayWhenDurationIsZero()
    {
        // Arrange
        var start = new DateTime(2024, 10, 1, 9, 0, 0);
        var end = new DateTime(2024, 10, 1, 12, 0, 0);
        const int durationInMinutes = 0;

        // Act
        var slots = Act(start, end, durationInMinutes);

        // Assert
        slots.Should().BeEmpty();
    }
}