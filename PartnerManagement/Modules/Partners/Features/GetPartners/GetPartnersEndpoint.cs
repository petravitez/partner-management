 using FastEndpoints;
    using System.Data;
    using Dapper;
    using PartnerManagement.Modules.Partners.Models;

namespace PartnerManagement.Modules.Partners.Features.GetPartners;

public class GetPartnersEndpoint : EndpointWithoutRequest<List<PartnerDetailsDto>>
{
    private readonly Func<IDbConnection> _dbFactory;

    public GetPartnersEndpoint(Func<IDbConnection> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public override void Configure()
    {
        Get("/partners");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Get all partners with policy data";
            s.Description = "Returns a list of all partners and their associated policy details";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        using var db = _dbFactory();

        var sql = @"
        SELECT 
            p.Id,    
            (p.FirstName + ' ' + p.LastName) AS Fullname,
            p.PartnerNumber,
            p.CroatianPIN,
            p.PartnerTypeId,
            p.CreatedAtUtc,
            p.CreatedByUser,
            p.IsForeign,
            g.Code AS Gender,
            CASE 
                WHEN ISNULL(pc.PolicyCount, 0) > 5 OR ISNULL(pc.TotalAmount, 0) > 5000 
                THEN CAST(1 AS BIT)
                ELSE CAST(0 AS BIT)
            END AS IsImportant
        FROM Partner p
        LEFT JOIN Gender g ON g.Id = p.GenderId
        LEFT JOIN (
            SELECT PartnerId,
                   COUNT(*) AS PolicyCount,
                   SUM(PolicyAmount) AS TotalAmount
            FROM Policy
            GROUP BY PartnerId
        ) pc ON pc.PartnerId = p.Id
        ORDER BY P.CreatedAtUtc DESC
    ";

        var result = await db.QueryAsync<PartnerDetailsDto>(sql);
        await SendAsync(result.ToList());
    }
}
