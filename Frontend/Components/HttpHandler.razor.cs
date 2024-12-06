using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Frontend.Components;

public partial class HttpHandler
{
    public static HttpHandler? Instance { get; private set; }
    [Inject] private HttpClient http { get; set; } = new();

    private CustomModal? InternalServerErrorModal;
    private CustomModal? BadRequestModal;
    private CustomModal? NotFoundModal;
    private static readonly JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    protected override void OnInitialized()
    {
        Instance = this;
    }

    public async Task<(HttpStatusCode, T?)> GetAsync<T>(string path, string JWTtoken, bool needDefualtCheck = true)
    {
        if (!string.IsNullOrEmpty(JWTtoken))
        {
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWTtoken);
        }
        HttpResponseMessage response = await http.GetAsync(path);
        return (response.StatusCode, Successful(response.StatusCode, HttpStatusCode.OK, needDefualtCheck) ?
            Deserialize<T>(await response.Content.ReadAsStringAsync()) : default);
    }

    public async Task<(HttpStatusCode, T?)> GetAsync<T>(string path, Dictionary<string, string> parameters, string JWTtoken, bool needDefualtCheck = true)
    {
        if (!string.IsNullOrEmpty(JWTtoken))
        {
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWTtoken);
        }

        if (parameters.Count > 0)
        {
            path = AddParameters(path, parameters);
        }

        HttpResponseMessage response = await http.GetAsync(path);
        return (response.StatusCode, Successful(response.StatusCode, HttpStatusCode.OK, needDefualtCheck) ?
            Deserialize<T>(await response.Content.ReadAsStringAsync()) : default);
    }

    public async Task<(HttpStatusCode, string)> PostAsync(string path, object dto, string JWTtoken, bool needDefualtCheck = true)
    {
        if (!string.IsNullOrEmpty(JWTtoken))
        {
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWTtoken);
        }
        HttpResponseMessage response = await http.PostAsync(path, Serialize(dto));
        Successful(response.StatusCode, HttpStatusCode.Created, needDefualtCheck);
        try
        {
            return (response.StatusCode, await response.Content.ReadAsStringAsync());
        }
        catch (Exception)
        {
            return (HttpStatusCode.NotImplemented, "Something went wrong, reading the response.");
        }
    }

    public async Task<(HttpStatusCode, T?)> PostAsync<T>(string path, object dto, string JWTtoken, bool needDefualtCheck = true)
    {
        if (!string.IsNullOrEmpty(JWTtoken))
        {
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWTtoken);
        }
        HttpResponseMessage response = await http.PostAsync(path, Serialize(dto));
        return (response.StatusCode, Successful(response.StatusCode, HttpStatusCode.Created, needDefualtCheck) ?
            Deserialize<T>(await response.Content.ReadAsStringAsync()) : default);
    }

    public async Task<HttpStatusCode> PutAsync(string path, object dto, string JWTtoken, bool needDefualtCheck = true)
    {
        if (!string.IsNullOrEmpty(JWTtoken))
        {
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWTtoken);
        }
        HttpResponseMessage response = await http.PutAsync(path, Serialize(dto));
        _ = Successful(response.StatusCode, HttpStatusCode.OK, needDefualtCheck);
        return response.StatusCode;
    }

    public async Task<(HttpStatusCode, T?)> PutAsync<T>(string path, object dto, string JWTtoken, bool needDefualtCheck = true)
    {
        if (!string.IsNullOrEmpty(JWTtoken))
        {
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWTtoken);
        }
        HttpResponseMessage response = await http.PutAsync(path, Serialize(dto));
        return (response.StatusCode, Successful(response.StatusCode, HttpStatusCode.OK, needDefualtCheck) ?
            Deserialize<T>(await response.Content.ReadAsStringAsync()) : default);
    }

    public async Task<HttpStatusCode> DeleteAsync(string path, string JWTtoken, bool needDefualtCheck = true)
    {
        if (!string.IsNullOrEmpty(JWTtoken))
        {
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWTtoken);
        }
        HttpResponseMessage response = await http.DeleteAsync(path);
        _ = Successful(response.StatusCode, HttpStatusCode.NoContent, needDefualtCheck);
        return response.StatusCode;
    }

    private static string AddParameters(string path, Dictionary<string, string> parameters)
    {
        path += "?";
        foreach (var item in parameters)
        {
            if (path.Last() != '?')
            {
                path += "&";
            }
            path += item.Key + "=" + item.Value;
        }
        return path;
    }

    private bool Successful(HttpStatusCode response, HttpStatusCode wants, bool needDefualtCheck)
    {
        if (needDefualtCheck)
        {
            switch (response)
            {
                case HttpStatusCode.BadRequest:
                    BadRequestModal?.Show();
                    return false;
                case HttpStatusCode.NotFound:
                    NotFoundModal?.Show();
                    return false;
                case HttpStatusCode.InternalServerError:
                    InternalServerErrorModal?.Show();
                    return false;

                default: break;
            }
        }

        return response == wants;
    }

    public static T? Deserialize<T>(string response)
    {
        try
        {
            return JsonSerializer.Deserialize<T>(response, options);
        }
        catch (Exception)
        {
            return default;
        }
    }

    private static StringContent Serialize(object toSerialize)
    {
        return new StringContent(
            JsonSerializer.Serialize(toSerialize),
            Encoding.UTF8,
            "application/json"
        );
    }
}
