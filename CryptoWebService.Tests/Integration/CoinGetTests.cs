using System.Net;
using CryptoWebService.API.Dtos;
using Newtonsoft.Json;

namespace CryptoWebService.Tests.Integration;

public class CoinGetTests
{
    private readonly HttpClient _client;
    
    public CoinGetTests()
    {
        var factory = new CustomWebApplicationFactory<Program>();
        _client = factory.CreateClient();
    }

    [Fact]
    public async void GetAllCoins_ReturnsAllCoins()
    {
        var response = await _client.GetAsync("/api/coin");
    
        var jsonBody = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<List<CoinGetDto>>(jsonBody);
        var expectedResponse = 1;
    
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(expectedResponse, result.Count);
    }
}