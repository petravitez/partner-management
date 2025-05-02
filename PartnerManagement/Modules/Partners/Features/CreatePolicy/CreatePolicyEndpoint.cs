using Dapper;
using FastEndpoints;
using PartnerManagement.Modules.Partners.Models;
using System.Data;

namespace PartnerManagement.Modules.Partners.Features.CreatePolicy;

public class CreatePolicyEndpoint : Endpoint<CreatePolicyRequest, PolicyDto>
{
    private readonly Func<IDbConnection> _dbFactory;

    public CreatePolicyEndpoint(Func<IDbConnection> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public override void Configure()
    {
        Post("/partners/{partnerId:int}/policies");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Create a new policy for a partner";
            s.Description = "Inserts a new policy into the database and returns the created policy.";
        });
    }

    public override async Task HandleAsync(CreatePolicyRequest req, CancellationToken ct)
    {
        using var db = _dbFactory();

        const string sql = @"
            INSERT INTO Policy (PartnerId, PolicyNumber, PolicyAmount)
            OUTPUT INSERTED.*
            VALUES (@PartnerId, @PolicyNumber, @PolicyAmount);
        ";

        var policy = await db.QuerySingleAsync<PolicyDto>(sql, req);

        await SendAsync(policy);
    }
}
