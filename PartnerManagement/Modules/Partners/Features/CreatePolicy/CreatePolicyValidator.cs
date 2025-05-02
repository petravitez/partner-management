using FluentValidation;
using Dapper;
using System.Data;
using FastEndpoints;

namespace PartnerManagement.Modules.Partners.Features.CreatePolicy;

public class CreatePolicyValidator : Validator<CreatePolicyRequest>
{
    private readonly Func<IDbConnection> _dbFactory;

    public CreatePolicyValidator(Func<IDbConnection> dbFactory)
    {
        _dbFactory = dbFactory;

        RuleFor(x => x.PartnerId)
            .MustAsync(PartnerExists)
            .WithMessage("Partner does not exist.");

        RuleFor(x => x.PolicyNumber)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(15);

        RuleFor(x => x.PolicyAmount)
            .GreaterThan(0);
    }

    private async Task<bool> PartnerExists(int partnerId, CancellationToken ct)
    {
        using var db = _dbFactory();
        const string sql = "SELECT COUNT(1) FROM Partner WHERE Id = @Id";
        int count = await db.ExecuteScalarAsync<int>(sql, new { Id = partnerId });
        return count > 0;
    }
}
