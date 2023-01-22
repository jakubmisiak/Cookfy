using Cookfy.Entities;
using FluentValidation;

namespace Cookfy.Models.Validators;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator(CookfyDbContext dbContext)
    {
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Password).MinimumLength(6);
        RuleFor(x => x.UserName)
            .Custom((value, context) =>
            {
                var userNameInUse = dbContext.Users.Any(x => x.UserName == value);
                if (userNameInUse)
                {
                    context.AddFailure("User name", "User name is taken");
                }
            });
    }   
}