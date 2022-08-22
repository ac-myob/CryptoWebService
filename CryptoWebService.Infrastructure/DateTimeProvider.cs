using CryptoWebService.Application.Abstractions;

namespace CryptoWebService.Infrastructure;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetNow() => DateTime.Now;
}