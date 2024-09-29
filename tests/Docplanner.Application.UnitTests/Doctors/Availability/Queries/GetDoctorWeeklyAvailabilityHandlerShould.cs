using Docplanner.Application.Doctors.Availability.Mappers;
using Docplanner.Application.Doctors.Availability.Queries;
using Docplanner.Contracts.Queries.Doctors.Availability;
using Docplanner.Tests.Shared.Factories.ExternalClients;
using Docplanner.Tests.Shared.Mocks.ExternalClients;
using FluentAssertions;

namespace Docplanner.Application.UnitTests.Doctors.Availability.Queries;

public class GetDoctorWeeklyAvailabilityHandlerShould
{
    private readonly AvailabilityClientMock _availabilityClientMock = new();
    private readonly GetDoctorWeeklyAvailabilityHandler _sut;

    public GetDoctorWeeklyAvailabilityHandlerShould()
    {
        _sut = new GetDoctorWeeklyAvailabilityHandler(_availabilityClientMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnDoctorWeeklyAvailabilityDto_WhenRequestIsValid()
    {
        // Arrange
        var request = new GetDoctorWeeklyAvailability(new DateTime(2024, 9, 30)); // Monday

        var availabilityResponse = GetAvailabilityResponsesFactory.Create(monday: DaysFactory.Create());

        _availabilityClientMock.SetupGetAvailabilitiesAsync(request.WeekStartDate, availabilityResponse);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(availabilityResponse.MapToDto(DateOnly.FromDateTime(request.WeekStartDate)));
    }
}