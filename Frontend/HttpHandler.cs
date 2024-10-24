using System.Net;
using System.Text;
using System.Text.Json;

namespace Frontend;

public static class HttpHandler
{
    public static async Task<(HttpStatusCode, string)> GetAsync(string path, HttpClient http)
    {
        HttpResponseMessage response = await http.GetAsync(path);
        return (response.StatusCode, await response.Content.ReadAsStringAsync());
    }

    public static async Task<(HttpStatusCode, string)> PostAsync(string path, object dto, HttpClient http)
    {
        StringContent content = new(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await http.PostAsync(path, content);
        return (response.StatusCode, await response.Content.ReadAsStringAsync());
    }

    public static async Task<(HttpStatusCode, string)> PutAsync(string path, object dto, HttpClient http)
    {
        StringContent content = new(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await http.PutAsync(path, content);
        return (response.StatusCode, await response.Content.ReadAsStringAsync());
    }

    public static async Task<(HttpStatusCode, string)> DeleteAsync(string path, HttpClient http)
    {
        HttpResponseMessage response = await http.DeleteAsync(path);
        return (response.StatusCode, await response.Content.ReadAsStringAsync());
    }
}
