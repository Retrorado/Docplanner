namespace Docplanner.Contracts.ExternalClients.AvailabilityClient.TakeSlot;

public class PatientDto
{
    public string Name { get; init; } = null!;
    public string SecondName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Phone { get; init; } = null!;
}