using Docplanner.Application.Doctors.Availability.Commands;
using Docplanner.Application.Doctors.Availability.Mappers;
using Docplanner.Tests.Shared.Factories.Commands;
using Docplanner.Tests.Shared.Mocks.ExternalClients;

namespace Docplanner.Application.UnitTests.Doctors.Availability.Commands;

public class TakeDoctorSlotHandlerShould
{
    private readonly AvailabilityClientMock _availabilityClientMock = new();
    private readonly TakeDoctorSlotHandler _sut;

    public TakeDoctorSlotHandlerShould()
    {
        _sut = new TakeDoctorSlotHandler(_availabilityClientMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCallTakeSlotAsync_WhenCommandIsValid()
    {
        // Arrange
        var command = TakeDoctorSlotCommandsFactory.Create();

        // Act
        await _sut.Handle(command, CancellationToken.None);

        // Assert
        _availabilityClientMock.AssertTakeSlotInAvailabilityClient(TakeSlotRequestMapper.FromCommand(command));
    }
}