using FluentValidation;

namespace NZWalks.API.Validators
{
    public class loginRequestValidator : AbstractValidator<Models.DTO.LoginRequest>
    {
        public loginRequestValidator()
        {
            RuleFor(x => x.username).NotEmpty();
            RuleFor(x => x.password).NotEmpty();

        }
    }
}
