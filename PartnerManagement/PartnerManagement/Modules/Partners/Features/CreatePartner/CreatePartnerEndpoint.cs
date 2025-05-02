using FastEndpoints;
using System.Data;
using Dapper;
using PartnerManagement.Modules.Partners.Models;

namespace PartnerManagement.Modules.Partners.Features.CreatePartner
{
    public class CreatePartnerEndpoint : Endpoint<CreatePartnerRequest, PartnerDto>
    {
        private readonly IDbConnection _db;

        public CreatePartnerEndpoint(IDbConnection db)
        {
            _db = db;
        }

        public override void Configure()
        {
            Post("/partners");
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Create a new partner";
                s.Description = "Inserts a new partner into the database and returns the created partner DTO.";
            });
        }

        public override async Task HandleAsync(CreatePartnerRequest req, CancellationToken ct)
        {
            var sql = @"
            INSERT INTO Partner 
            (FirstName, LastName, Address, PartnerNumber, CroatianPIN, PartnerTypeId,
             CreatedAtUtc, CreatedByUser, IsForeign, ExternalCode, GenderId)
            OUTPUT INSERTED.*
            VALUES (@FirstName, @LastName, @Address, @PartnerNumber, @CroatianPIN, @PartnerTypeId,
                    GETUTCDATE(), @CreatedByUser, @IsForeign, @ExternalCode, @GenderId);
        ";

            PartnerDto partner = await _db.QuerySingleAsync<PartnerDto>(sql, req);
            await SendAsync(partner);
        }
    }

}
