using System.Net.Http.Json;

namespace SharedClass
{
    public class BaseApi
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7170/";

        public BaseApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetDataAsync(string endpoint)
        {
            // Combine the base URL with the endpoint
            var fullUrl = $"{_baseUrl}/{endpoint}";

            // Make the HTTP request
            var response = await _httpClient.GetStringAsync(fullUrl);

            return response;
        }

        public async Task<List<T>> GetDataAsync<T>(string endpoint)
        {
            var fullUrl = $"{_baseUrl}{endpoint}";

            //Make the HTTp request and deserialize the response to a List<T>
            var response = await _httpClient.GetFromJsonAsync<List<T>>(fullUrl);
            
            return response;
        }

        public async Task<HttpResponseMessage> PostDataAsync<T>(string endpoint, T data)
        {
            var fullUrl = $"{_baseUrl}{endpoint}";
            var response = await _httpClient.PostAsJsonAsync(fullUrl, data);
            return response;
        }

        public async Task<HttpResponseMessage> PutDataAsync<T>(string endpoint, T data)
        {
            var fullUrl = $"{_baseUrl}{endpoint}";
            var response = await _httpClient.PutAsJsonAsync<T>(fullUrl, data);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteDataAsync(string endpoint)
        {
            var fullUrl = $"{_baseUrl}{endpoint}";
            var response = await _httpClient.DeleteAsync(fullUrl);
            return response;
        }
    }
}