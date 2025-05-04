namespace PartnerManagement.Modules.Partners.Features.GetPartnersDropdown
{
    using FastEndpoints;
    using System.Data;
    using Dapper;
    using PartnerManagement.Modules.Partners.Models;

    public class GetPartnersDropdownEndpoint : EndpointWithoutRequest<List<PartnerDropdownDto>>
    {
        private readonly Func<IDbConnection> _dbFactory;

        public GetPartnersDropdownEndpoint(Func<IDbConnection> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public override void Configure()
        {
            Get("/partners/dropdown");
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Get all partners for dropdown";
                s.Description = "Returns partner id and full name for use in dropdowns.";
            });
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            using var db = _dbFactory();
            var sql = @"
            SELECT
                p.Id,
                (p.FirstName + ' ' + p.LastName) AS FullName
            FROM Partner p
            ORDER BY p.FirstName, p.LastName
        ";

            var result = await db.QueryAsync<PartnerDropdownDto>(sql);
            await SendAsync(result.ToList());
        }
    }

}
