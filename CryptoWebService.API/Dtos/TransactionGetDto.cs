using FluentValidation;

namespace CryptoWebService.API.Dtos;

public class TransactionGetDto : TransactionPostPutDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
}

public class TransactionGetDtoValidator : AbstractValidator<TransactionGetDto>
{
    public TransactionGetDtoValidator()
    {
        RuleFor(t => t.Date).NotEmpty();
    }
}