namespace API.UnitTest
{
    public static class UnitTestHelper
    {
        private static HttpClient _httpClient = new()
        {
            BaseAddress = new Uri("https://localhost:7281/api/")
        };
        public static async Task<string> TryCatchAsync(Func<Task<string>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return "";
        }

        public static async Task<string> GetAsync(string url)
        {
            return await (await _httpClient.GetAsync(url)).Content.ReadAsStringAsync();
        }

        public static async Task<string> PostAsync(string url, StringContent content)
        {
            return await (await _httpClient.PostAsync(url, content)).Content.ReadAsStringAsync();
        }

        public static async Task<string> PutAsync(string url, StringContent content)
        {
            return await (await _httpClient.PutAsync(url, content)).Content.ReadAsStringAsync();
        }

        public static async Task<string> DeleteAsync(string url)
        {
            return await (await _httpClient.DeleteAsync(url)).Content.ReadAsStringAsync();
        }
    }
}
