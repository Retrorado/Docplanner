using Docplanner.Contracts.Commands.Doctors.Availability;

namespace Docplanner.Tests.Shared.Factories.Commands;

public static class TakeDoctorSlotCommandsFactory
{
    public static TakeDoctorSlot Create(
        DateTime? start = null,
        DateTime? end = null,
        string comment = "Test comment",
        PatientDto? patient = null,
        Guid? facilityId = null)
    {
        return new TakeDoctorSlot
        {
            Start = start ?? DateTime.Now,
            End = end ?? DateTime.Now.AddHours(1),
            Comment = comment,
            Patient = patient ?? new PatientDto
            {
                Name = "John",
                SecondName = "Doe",
                Email = "john.doe@example.com",
                Phone = "123456789"
            },
            FacilityId = facilityId ?? Guid.NewGuid()
        };
    }

    public static TakeDoctorSlot CreateWithEmptyPatient(
        DateTime? start = null,
        DateTime? end = null,
        string comment = "Test comment",
        Guid? facilityId = null)
    {
        return new TakeDoctorSlot
        {
            Start = start ?? DateTime.Now,
            End = end ?? DateTime.Now.AddHours(1),
            Comment = comment,
            Patient = null,
            FacilityId = facilityId ?? Guid.NewGuid()
        };
    }
}