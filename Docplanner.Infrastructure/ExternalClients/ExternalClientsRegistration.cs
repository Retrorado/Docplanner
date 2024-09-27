using System.Net.Http.Headers;
using Docplanner.Application.ExternalClients;
using Docplanner.Infrastructure.ExternalClients.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Docplanner.Infrastructure.ExternalClients;

internal static class ExternalClientsRegistration
{
    internal static IServiceCollection AddExternalClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAvailabilityClient, AvailabilityClient>();

        var clientConfiguration = GetClientConfiguration(configuration, AvailabilityClientConfiguration.Key);

        services.AddHttpClient<IAvailabilityClient, AvailabilityClient>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(clientConfiguration.BaseAddress ?? string.Empty);
            var byteArray = System.Text.Encoding.UTF8.GetBytes($"{clientConfiguration.Username}:{clientConfiguration.Password}");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        });

        return services;
    }

    private static ExternalClientConfiguration GetClientConfiguration(IConfiguration configuration, string clientName)
    {
        var clientConfiguration = configuration
            .GetSection(ExternalClientConfiguration.SectionName)
            .GetSection(clientName)
            .Get<ExternalClientConfiguration>();

        if (string.IsNullOrWhiteSpace(clientConfiguration!.BaseAddress))
        {
            throw new ArgumentException("External clients are enabled but no base address for Borat was provided.");
        }

        return clientConfiguration;
    }
}