 using FastEndpoints;
    using System.Data;
    using Dapper;
    using PartnerManagement.Modules.Partners.Models;

namespace PartnerManagement.Modules.Partners.Features.GetPartners;

public class GetPartnersEndpoint : Endpoint<GetPartnersRequest, GetPartnersResponse>
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
    }

    public override async Task HandleAsync(GetPartnersRequest req, CancellationToken ct)
    {
        using var db = _dbFactory();

        var offset = (req.Page - 1) * req.PageSize;

        var sql = @"
        SELECT 
            p.Id,    
            (p.FirstName + ' ' + p.LastName) AS Fullname,
            p.Address,
            p.ExternalCode,
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
        ORDER BY p.CreatedAtUtc DESC
        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

        SELECT COUNT(*) FROM Partner;
        ";

        using var multi = await db.QueryMultipleAsync(sql, new { Offset = offset, req.PageSize });
        var items = (await multi.ReadAsync<PartnerDetailsDto>()).ToList();
        var total = await multi.ReadFirstAsync<int>();

        await SendAsync(new GetPartnersResponse
        {
            Items = items,
            TotalCount = total
        });
    }
}
