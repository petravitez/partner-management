 using FastEndpoints;
    using System.Data;
    using Dapper;
    using PartnerManagement.Modules.Partners.Models;

namespace PartnerManagement.Modules.Partners.Features.GetPartners
{


    public class GetPartnersEndpoint : EndpointWithoutRequest<List<PartnerDto>>
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
                s.Summary = "Get all partners";
                s.Description = "Returns a list of all partners in the database";
            });
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            using var db = _dbFactory(); 
            var partners = await db.QueryAsync<PartnerDto>("SELECT * FROM Partner");
            await SendAsync(partners.ToList());
        }
    }


}
