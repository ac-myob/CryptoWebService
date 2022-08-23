using CryptoWebService.Domain.Models;

namespace CryptoWebService.Tests;

public static class SampleCoins
{
    public static Coin GetOne() => new() {Id = 1, Name = "SingleCoin", Symbol = "X"};

    public static IEnumerable<Coin> GetMany(int numberOfCoins)
    {
        return Enumerable.Range(1, numberOfCoins + 1)
            .Select(i => new Coin {Id = i, Name = $"Coin{i}", Symbol = $"COIN{i}"});
    }
}
