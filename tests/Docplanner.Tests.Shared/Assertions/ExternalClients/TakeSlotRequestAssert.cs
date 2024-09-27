using Docplanner.Contracts.Commands.Doctors.Availability;
using Docplanner.Contracts.ExternalClients.AvailabilityClient.TakeSlot;
using FluentAssertions;

namespace Docplanner.Tests.Shared.Assertions.ExternalClients;

public static class TakeSlotRequestAssert
{
    public static TakeSlotRequest ShouldMatch(this TakeSlotRequest request, TakeDoctorSlot command)
    {
        request.Start.Should().Be(command.Start);
        request.End.Should().Be(command.End);
        request.Comment.Should().Be(command.Comment);
        request.Patient.Name.Should().Be(command.Patient.Name);
        request.Patient.SecondName.Should().Be(command.Patient.SecondName);
        request.Patient.Email.Should().Be(command.Patient.Email);
        request.Patient.Phone.Should().Be(command.Patient.Phone);
        request.FacilityId.Should().Be(command.FacilityId);

        return request;
    }
}