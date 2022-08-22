namespace CryptoWebService.Application.Abstractions;

public interface IDateTimeProvider
{
    public DateTime GetNow();
}