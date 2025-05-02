namespace PartnerManagement.Modules.Partners.Models;

public record PolicyDto(
 int Id,
 int PartnerId,
 string PolicyNumber,
 decimal PolicyAmount
);
