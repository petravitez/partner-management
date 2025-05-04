using Dapper;
using FastEndpoints;
using System.Data;

namespace PartnerManagement.Modules.Partners.Features.CheckExternalCode
{
    public class CheckExternalCodeUniqueEndpoint : Endpoint<CheckExternalCodeUniqueRequest, CheckExternalCodeUniqueResponse>
    {
        private readonly Func<IDbConnection> _dbFactory;

        public CheckExternalCodeUniqueEndpoint(Func<IDbConnection> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public override void Configure()
        {
            Get("/partners/check-external-code");
            AllowAnonymous(); 
            Summary(s =>
            {
                s.Summary = "Check if ExternalCode is unique";
                s.Description = "Returns true if the given ExternalCode does not exist in the Partner table.";
            });
        }

        public override async Task HandleAsync(CheckExternalCodeUniqueRequest req, CancellationToken ct)
        {
            using var db = _dbFactory();
            var sql = "SELECT COUNT(1) FROM Partner WHERE ExternalCode = @ExternalCode";

            var count = await db.ExecuteScalarAsync<int>(sql, new { req.ExternalCode });

            await SendAsync(new CheckExternalCodeUniqueResponse
            {
                IsUnique = count == 0
            });
        }
    }

}
