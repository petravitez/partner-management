using FastEndpoints;
using FluentValidation;

namespace PartnerManagement.Modules.Partners.Features.CreatePartner
{

    public class CreatePartnerValidator : Validator<CreatePartnerRequest>
    {
        public CreatePartnerValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(255);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(255);

            RuleFor(x => x.PartnerNumber)
                .NotEmpty()
                .Length(20);

            RuleFor(x => x.CroatianPIN)
                .Matches(@"^\d{11}$").When(x => !string.IsNullOrWhiteSpace(x.CroatianPIN));

            RuleFor(x => x.PartnerTypeId)
                .Must(x => x == 1 || x == 2)
                .WithMessage("Must be 1 (Personal) or 2 (Legal)");

            RuleFor(x => x.CreatedByUser)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(255);

            RuleFor(x => x.ExternalCode)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(20);

            RuleFor(x => x.GenderId)
                .GreaterThan(0);

        }
    }

}
