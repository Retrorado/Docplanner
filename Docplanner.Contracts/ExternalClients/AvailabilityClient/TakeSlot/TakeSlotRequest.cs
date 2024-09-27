namespace Docplanner.Contracts.ExternalClients.AvailabilityClient.TakeSlot;

public class TakeSlotRequest
{
    public DateTime Start { get; init; }
    public DateTime End { get; init; }
    public string Comment { get; init; } = null!;
    public PatientDto Patient { get; init; } = null!;
    public Guid FacilityId { get; init; } = Guid.NewGuid();
}