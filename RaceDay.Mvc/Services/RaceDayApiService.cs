
using System.Net.Http.Json;

namespace RaceDay.Mvc.Services
{
    public class RaceDayApiService
    {
        private readonly HttpClient _httpClient;

        public RaceDayApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Retrieve information from the API
        public async Task<T?> GetAsync<T>(string endpoint)
        {
            return await _httpClient.GetFromJsonAsync<T>(endpoint);
        }

        // POST: Send information to the API and receive a response
        public async Task<TResponse?> PostAsync<TRequest, TResponse>(
            string endpoint,
            TRequest data)
        {
            var response = await _httpClient.PostAsJsonAsync(
                endpoint, data);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        // POST: Send information without expecting a response body
        public async Task PostAsync<TRequest>(
            string endpoint,
            TRequest data)
        {
            var response = await _httpClient.PostAsJsonAsync(
                endpoint, data);

            response.EnsureSuccessStatusCode();
        }
    }
}
