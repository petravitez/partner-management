namespace PartnerManagement.Modules.Partners.Models
{
    public class PartnerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string PartnerNumber { get; set; } = string.Empty;
        public string? CroatianPIN { get; set; }
        public byte PartnerTypeId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public string CreatedByUser { get; set; } = string.Empty;
        public bool IsForeign { get; set; }
        public string ExternalCode { get; set; } = string.Empty;
        public int GenderId { get; set; }
    }

}
