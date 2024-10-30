using System.Net;
using System.Text;
using System.Text.Json;

namespace Frontend;

public static class HttpHandler
{
    private static readonly JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static async Task<(HttpStatusCode, T?)> GetAsync<T>(string path, HttpClient http)
    {
        HttpResponseMessage response = await http.GetAsync(path);
        return (response.StatusCode, Deserialize<T>(await response.Content.ReadAsStringAsync()));
    }

    public static async Task<(HttpStatusCode, string)> PostAsync(string path, object dto, HttpClient http)
    {
        HttpResponseMessage response = await http.PostAsync(path, Serialize(dto));
        return (response.StatusCode, await response.Content.ReadAsStringAsync());
    }

    public static async Task<(HttpStatusCode, T?)> PostAsync<T>(string path, object dto, HttpClient http)
    {
        HttpResponseMessage response = await http.PostAsync(path, Serialize(dto));
        return (response.StatusCode, Deserialize<T>(await response.Content.ReadAsStringAsync()));
    }

    public static async Task<HttpStatusCode> PutAsync(string path, object dto, HttpClient http)
    {
        HttpResponseMessage response = await http.PutAsync(path, Serialize(dto));
        return response.StatusCode;
    }

    public static async Task<(HttpStatusCode, T?)> PutAsync<T>(string path, object dto, HttpClient http)
    {
        HttpResponseMessage response = await http.PutAsync(path, Serialize(dto));
        return (response.StatusCode, Deserialize<T>(await response.Content.ReadAsStringAsync()));
    }

    public static async Task<HttpStatusCode> DeleteAsync(string path, HttpClient http)
    {
        HttpResponseMessage response = await http.DeleteAsync(path);
        return response.StatusCode;
    }

    public static T? Deserialize<T>(string response)
    {
        return JsonSerializer.Deserialize<T>(response, options);
    }

    private static StringContent Serialize(object toSerialize)
    {
        return new StringContent(JsonSerializer.Serialize(toSerialize), Encoding.UTF8, "application/json");
    }
}
