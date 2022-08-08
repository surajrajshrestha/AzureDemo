using Microsoft.Identity.Web;
using System.Net.Http.Headers;

namespace Vision.Web
{
    public interface ITokenService {
        Task<string> GetTokenAsync();
    }

    public class TokenService : ITokenService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiScope = string.Empty;
        private readonly string _baseAddress = string.Empty;
        private readonly ITokenAcquisition _tokenAcquisition;
        public TokenService(HttpClient httpClient, ITokenAcquisition tokenAcquisition, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _tokenAcquisition = tokenAcquisition;
            _apiScope = configuration["APIConfig:APIScope"];
            _baseAddress = configuration["APIConfig:APIBaseAddress"];
        }

        public async Task<string> GetTokenAsync()
        {
            await FindTokenAsync();
            var response = await _httpClient.GetAsync($"{_baseAddress}/weatherforecast");
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var output = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(content);
                return output;
            }
            throw new HttpRequestException("Invalid response");
        }

        private async Task FindTokenAsync()
        {
            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { _apiScope });
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
