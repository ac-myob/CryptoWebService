using System.Text.RegularExpressions;
using FluentValidation;

namespace CryptoWebService.API.Dtos;

public class UserPostPutDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class UserPostPutDtoValidator : AbstractValidator<UserPostPutDto>
{
    public UserPostPutDtoValidator()
    {
        RuleFor(u => u.FirstName).Length(1, 50).Must(IsInNameFormat)
            .WithMessage(u => InvalidNameMessage(u.FirstName));
        RuleFor(u => u.LastName).Length(1, 50).Must(IsInNameFormat)
            .WithMessage(u => InvalidNameMessage(u.LastName));
    }

    private static bool IsInNameFormat(string name) => Regex.IsMatch(name, @"^[A-Z][A-Za-z]*");

    private static string InvalidNameMessage(string name) =>
        $"The name '{name}' must begin with a capital letter, followed by zero or more lower case letters";
}
