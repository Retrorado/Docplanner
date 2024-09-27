namespace Docplanner.Contracts.Queries.Doctors.Availability;

public class DoctorWeeklyAvailabilityDto
{
    public Day Monday { get; init; } = null!;
    public Day Tuesday { get; init; } = null!;
    public Day Wednesday { get; init; } = null!;
    public Day Thursday { get; init; } = null!;
    public Day Friday { get; init; } = null!;
    public Day Saturday { get; init; } = null!;
    public Day Sunday { get; init; } = null!;
    public FacilityDto Facility { get; init; } = null!;
}