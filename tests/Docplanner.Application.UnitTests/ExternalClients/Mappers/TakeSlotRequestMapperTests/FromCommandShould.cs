using Docplanner.Application.Doctors.Availability.Mappers;
using Docplanner.Contracts.Commands.Doctors.Availability;
using Docplanner.Contracts.ExternalClients.AvailabilityClient.TakeSlot;
using Docplanner.Tests.Shared.Assertions.ExternalClients;
using Docplanner.Tests.Shared.Factories.Commands;

namespace Docplanner.Application.UnitTests.ExternalClients.Mappers.TakeSlotRequestMapperTests;

public class FromCommandShould
{
    private static TakeSlotRequest Act(TakeDoctorSlot command)
        => TakeSlotRequestMapper.FromCommand(command);

    [Fact]
    public void ReturnProperlyMappedRequest_WhenAllFieldsAreProvided()
    {
        // Arrange
        var command = TakeDoctorSlotCommandsFactory.Create();

        // Act
        var result = Act(command);

        // Assert
        result.ShouldMatch(command);
    }
}