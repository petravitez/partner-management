using FastEndpoints;
using System.Data;
using Dapper;
using PartnerManagement.Modules.Partners.Models;

namespace PartnerManagement.Modules.Partners.Features.GetPartner
{


    public class GetPartnerEndpoint : Endpoint<GetPartnerRequest, PartnerDto>
    {
        private readonly Func<IDbConnection> _dbFactory;

        public GetPartnerEndpoint(Func<IDbConnection> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public override void Configure()
        {
            Get("/partners/{id:int}");
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Get a partner by ID";
                s.Description = "Returns a single partner based on the given ID";
            });
        }

        public override async Task HandleAsync(GetPartnerRequest req, CancellationToken ct)
        {
            using var db = _dbFactory(); 
            var partner = await db.QueryFirstOrDefaultAsync<PartnerDto>(
                "SELECT * FROM Partner WHERE Id = @Id", new { req.Id });

            if (partner is null)
            {
                await SendNotFoundAsync();
                return;
            }

            await SendAsync(partner);
        }
    }


}
