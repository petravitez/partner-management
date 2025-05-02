namespace PartnerManagement.Modules.Partners.Features.CreatePartner
{
    public record CreatePartnerRequest(
    string FirstName,
    string LastName,
    string? Address,
    string PartnerNumber,
    string? CroatianPIN,
    int PartnerTypeId,
    string CreatedByUser,
    bool IsForeign,
    string ExternalCode,
    int GenderId
);


}
