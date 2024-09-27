using Docplanner.Application.ExternalClients;
using Docplanner.Contracts.ExternalClients.AvailabilityClient.GetAvailability;
using Docplanner.Contracts.ExternalClients.AvailabilityClient.TakeSlot;
using Microsoft.Extensions.Logging;

namespace Docplanner.Infrastructure.ExternalClients;

public class AvailabilityClient : HttpClientBase, IAvailabilityClient
{
    public AvailabilityClient(ILoggerFactory loggerFactory, HttpClient httpClient) : base(loggerFactory, httpClient)
    {
    }

    public Task<GetAvailabilityResponse> GetAvailabilitiesAsync(DateTime startDate, CancellationToken cancellationToken)
    {
        var requestDate = startDate.ToString("yyyyMMdd");
        return GetAsync<GetAvailabilityResponse>($"GetWeeklyAvailability/{requestDate}", cancellationToken);
    }

    public async Task TakeSlotAsync(TakeSlotRequest request, CancellationToken cancellationToken)
    {
        await PostAsync("TakeSlot", request, cancellationToken);
    }
}