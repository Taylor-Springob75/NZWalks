using FluentValidation;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Validators
{
    public class AddRegionRequestValidator : AbstractValidator<Models.DTO.AddRegionRequest>
    {
        public AddRegionRequestValidator()
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
