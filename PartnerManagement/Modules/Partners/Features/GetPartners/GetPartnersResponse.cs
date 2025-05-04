using PartnerManagement.Modules.Partners.Models;

namespace PartnerManagement.Modules.Partners.Features.GetPartners
{
    public class GetPartnersResponse
    {
        public List<PartnerDetailsDto> Items { get; set; } = new();
        public int TotalCount { get; set; }
    }
}
