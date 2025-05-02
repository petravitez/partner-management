using System.Data;
using Dapper;
using FastEndpoints;
using FluentValidation;
using PartnerManagement.Helpers;

namespace PartnerManagement.Modules.Partners.Features.CreatePartner
{

    public class CreatePartnerValidator : Validator<CreatePartnerRequest>
    {
        private readonly Func<IDbConnection> _dbFactory;

        public CreatePartnerValidator(Func<IDbConnection> dbFactory)
        {
            _dbFactory = dbFactory;

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
                    .Must(OibValidator.IsValid)
                    .When(x => !string.IsNullOrWhiteSpace(x.CroatianPIN))
                    .WithMessage("Croatian OIB is not valid.");

            RuleFor(x => x.PartnerTypeId)
           .MustAsync(ValidPartnerType)
           .WithMessage("Invalid PartnerTypeId. Must reference an existing PartnerType.");

            RuleFor(x => x.CreatedByUser)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(255);

            RuleFor(x => x.ExternalCode)
             .NotEmpty()
             .MinimumLength(10)
             .MaximumLength(20)
             .MustAsync(BeUniqueExternalCode)
             .WithMessage("ExternalCode must be unique.");

            RuleFor(x => x.GenderId)
            .MustAsync(ValidGenderId)
            .WithMessage("Invalid GenderId. Must reference an existing Gender.");

        }


        private async Task<bool> ValidPartnerType(byte partnerTypeId, CancellationToken ct)
        {
            using var db = _dbFactory();
            var sql = "SELECT COUNT(1) FROM PartnerType WHERE Id = @Id";
            int count = await db.ExecuteScalarAsync<int>(sql, new { Id = partnerTypeId });
            return count > 0;
        }

        private async Task<bool> ValidGenderId(int genderId, CancellationToken ct)
        {
            using var db = _dbFactory();
            var sql = "SELECT COUNT(1) FROM Gender WHERE Id = @Id";
            int count = await db.ExecuteScalarAsync<int>(sql, new { Id = genderId });
            return count > 0;
        }

        private async Task<bool> BeUniqueExternalCode(string externalCode, CancellationToken ct)
        {
            using var db = _dbFactory();
            var sql = "SELECT COUNT(1) FROM Partner WHERE ExternalCode = @ExternalCode";
            int count = await db.ExecuteScalarAsync<int>(sql, new { ExternalCode = externalCode });
            return count == 0;
        }


    }
}
