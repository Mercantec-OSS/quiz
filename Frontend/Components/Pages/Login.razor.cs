using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Identity.Data;
using System.Text;
using API.Models;

namespace Frontend.Components.Pages
{
    public partial class Login
    {
        public API.Models.LoginRequest LoginRequest = new API.Models.LoginRequest();
        


        private HttpClient client = new HttpClient() { BaseAddress = new Uri("https://mercantec-quiz.onrender.com")};
        
        public async Task HandleLogin()
        {

            string json = System.Text.Json.JsonSerializer.Serialize(LoginRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/Users/login", content);
            if (response.IsSuccessStatusCode) Navigation.NavigateTo("/");
        }
    }
}
