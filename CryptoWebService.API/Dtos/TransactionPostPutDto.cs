using CryptoWebService.Domain;
using FluentValidation;

namespace CryptoWebService.API.Dtos;

public class TransactionPostPutDto
{
    public int UserId { get; set; }
    public int CoinId { get; set; }
    public TransactionType TransactionType { get; set; }
    public double Quantity { get; set; }
    public double Price { get; set; }
}

public class TransactionPostPutDtoValidator : AbstractValidator<TransactionPostPutDto>
{
    public TransactionPostPutDtoValidator()
    {
        RuleFor(t => t.UserId).NotEmpty().GreaterThan(0);
        RuleFor(t => t.CoinId).NotEmpty().GreaterThan(0);
        RuleFor(t => t.TransactionType).IsInEnum();
        RuleFor(t => t.Quantity).GreaterThan(0);
        RuleFor(t => t.Price).GreaterThan(0);
    }
}
