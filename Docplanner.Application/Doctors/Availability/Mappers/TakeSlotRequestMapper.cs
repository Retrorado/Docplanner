using Docplanner.Contracts.Commands.Doctors.Availability;
using Docplanner.Contracts.ExternalClients.AvailabilityClient.TakeSlot;
using PatientDto = Docplanner.Contracts.ExternalClients.AvailabilityClient.TakeSlot.PatientDto;

namespace Docplanner.Application.Doctors.Availability.Mappers;

public static class TakeSlotRequestMapper
{
    public static TakeSlotRequest FromCommand(TakeDoctorSlot command)
    {
        return new TakeSlotRequest
        {
            Start = command.Start,
            End = command.End,
            Comment = command.Comment,
            Patient = new PatientDto
            {
                Name = command.Patient.Name,
                SecondName = command.Patient.SecondName,
                Email = command.Patient.Email,
                Phone = command.Patient.Phone
            },
            FacilityId = command.FacilityId
        };
    }
}