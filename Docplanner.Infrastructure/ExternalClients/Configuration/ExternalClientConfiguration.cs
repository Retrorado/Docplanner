namespace Docplanner.Infrastructure.ExternalClients.Configuration;

public class ExternalClientConfiguration
{
    public const string SectionName = "ExternalClients";
    public string? BaseAddress { get; init; }
    public string? Username { get; init; }
    public string? Password { get; init; }
}