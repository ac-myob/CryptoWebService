using CryptoWebService.Domain.Models;

namespace CryptoWebService.Tests;

public static class SampleCoins
{
    public static Coin First => new() {Id = 1, Name = "Bitcoin", Symbol = "BTC"};
    public static Coin Second => new() {Id = 2, Name = "Ethereum", Symbol = "ETH"};
    public static Coin Third => new() {Id = 3, Name = "Cardano", Symbol = "ADA"};
    public static Coin Fourth => new() {Id = 4, Name = "Polygon", Symbol = "MATIC"};
    public static Coin Fifth => new() {Id = 5, Name = "Polkadot", Symbol = "DOT"};
}
