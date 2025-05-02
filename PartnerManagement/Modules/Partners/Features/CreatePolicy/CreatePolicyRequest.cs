namespace PartnerManagement.Modules.Partners.Features.CreatePolicy;

public record CreatePolicyRequest(
int PartnerId,
string PolicyNumber,
decimal PolicyAmount
);
