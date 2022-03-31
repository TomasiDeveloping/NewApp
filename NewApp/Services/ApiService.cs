using NewApp.Models;
using Newtonsoft.Json;

namespace NewApp.Services
{
    public class ApiService
    {
        public async Task<Root> GetNews(string categoryName)
        {
            var httpClient = new HttpClient();
            var gnewsURI = "https://gnews.io/api/v4/top-headlines?";

            // Get Token from https://gnews.io/
            var gnewsApiKey = "YOUR_API_KEY";
            var language = "de";
            var country = "ch";
            var response = await httpClient.GetStringAsync($"{gnewsURI}token={gnewsApiKey}&lang={language}&country={country}&topic={categoryName.ToLower()}");

            return JsonConvert.DeserializeObject<Root>(response);
        }
    }
}
