using FluentValidation;

namespace CryptoWebService.API.Dtos;

public class CoinPostPutDto
{
    public string Name { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
}

public class CoinPostPutDtoValidator : AbstractValidator<CoinPostPutDto>
{
    public CoinPostPutDtoValidator()
    {
        RuleFor(c => c.Name).NotEmpty().NotNull();
        RuleFor(c => c.Symbol)
            .NotEmpty().NotNull()
            .Must(symbol => symbol.All(char.IsUpper))
            .WithMessage("Coin symbol must be in capital letters.");;
    }
}