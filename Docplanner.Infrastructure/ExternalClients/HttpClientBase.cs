using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;

namespace Docplanner.Infrastructure.ExternalClients;

public abstract class HttpClientBase(ILoggerFactory loggerFactory, HttpClient httpClient)
{
    private readonly ILogger<HttpClientBase> _logger = loggerFactory.CreateLogger<HttpClientBase>()
                                                       ?? throw new ArgumentNullException(nameof(loggerFactory));

    protected Task<T> GetAsync<T>(string url, CancellationToken cancellationToken) where T : class
    {
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);
        return SendAndExtractResponse<T>(httpRequestMessage, cancellationToken);
    }

    protected async Task PostAsync(string url, object data, CancellationToken cancellationToken)
    {
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(
                JsonSerializer.Serialize(data, options: SerializerOptions),
                Encoding.UTF8,
                MediaTypeNames.Application.Json)
        };

        HttpResponseMessage? response = null;
        try
        {
            response = await httpClient.SendAsync(httpRequestMessage, cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception)
        {
            await LogError(httpRequestMessage, response);
            throw;
        }
    }

    private async Task LogError(HttpRequestMessage request, HttpResponseMessage? response)
    {
        var responseMessage = "No response";
        if (response != null)
        {
            responseMessage = await response.Content.ReadAsStringAsync(CancellationToken.None);
        }

        _logger.LogError(
            "Request to '{url}' failed. Response: {responseMessage}",
            request.RequestUri?.ToString(),
            responseMessage);
    }

    private async Task<T> SendAndExtractResponse<T>(HttpRequestMessage request, CancellationToken cancellationToken) where T : class
    {
        HttpResponseMessage? response = null;
        try
        {
            response = await httpClient.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception)
        {
            await LogError(request, response);
            throw;
        }

        return (await ExtractJsonResponse<T>(response, cancellationToken))!;
    }

    private static Task<T?> ExtractJsonResponse<T>(HttpResponseMessage response, CancellationToken cancellationToken) where T : class
    {
        return response.Content.ReadFromJsonAsync<T>(
            cancellationToken: cancellationToken,
            options: SerializerOptions);
    }

    private static JsonSerializerOptions SerializerOptions
        => new(JsonSerializerDefaults.Web)
        {
            Converters = { new JsonStringEnumConverter() }
        };
}