using Docplanner.Application.ExternalClients;
using Docplanner.Contracts.ExternalClients.AvailabilityClient.GetAvailability;
using Docplanner.Contracts.ExternalClients.AvailabilityClient.TakeSlot;
using Moq;

namespace Docplanner.Tests.Shared.Mocks.ExternalClients;

public class AvailabilityClientMock
{
    public IAvailabilityClient Object => Mock.Object;

    public readonly Mock<IAvailabilityClient> Mock = new();

    public void SetupGetAvailabilitiesAsync(DateTime startDate, GetAvailabilityResponse response)
    {
        Mock
            .Setup(client => client.GetAvailabilitiesAsync(
                startDate,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
    }

    public void AssertTakeSlotInAvailabilityClient(TakeSlotRequest request)
    {
        Mock.Verify(
            client => client.TakeSlotAsync(
                It.Is<TakeSlotRequest>(r => r.Start == request.Start
                                            && r.End == request.End
                                            && r.FacilityId == request.FacilityId
                                            && r.Comment == request.Comment
                                            && r.Patient.Name == request.Patient.Name
                                            && r.Patient.SecondName == request.Patient.SecondName
                                            && r.Patient.Email == request.Patient.Email
                                            && r.Patient.Phone == request.Patient.Phone),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}