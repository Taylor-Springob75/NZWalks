using FluentValidation;

namespace NZWalks.API.Validators
{
    public class UpdateRegionRequestValidator : AbstractValidator<Models.DTO.UpdateRegionRequest>
    {
        public UpdateRegionRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Area).GreaterThan(0);
            RuleFor(x => x.Lat).GreaterThanOrEqualTo(-90).LessThanOrEqualTo(90);
            RuleFor(x => x.Long).GreaterThanOrEqualTo(-180).LessThanOrEqualTo(180);
            RuleFor(x => x.Population).GreaterThanOrEqualTo(0);
        }
    }
}
