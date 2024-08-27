using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Identity.Data;
using System.Text;
using API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Frontend.Components.Pages
{
    public partial class Login
    {
        public API.Models.LoginRequest LoginRequest = new API.Models.LoginRequest();
        


        private HttpClient client = new HttpClient() { BaseAddress = new Uri("https://mercantec-quiz.onrender.com")};
        
        public async Task HandleLogin()
        {
            Console.WriteLine($"Email: {LoginRequest.Email}, Password: {LoginRequest.Password}");
            string json = System.Text.Json.JsonSerializer.Serialize(LoginRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsJsonAsync("api/Users/login", LoginRequest);
            if (response.IsSuccessStatusCode) Navigation.NavigateTo("/");
        }
    }
}
