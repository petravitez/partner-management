namespace PartnerManagement.Modules.Partners.Models
{
    public record PartnerDto
    {
        public int Id { get; set; }
        public required string FirstName { get; set; } 
        public required string LastName { get; set; } 
        public string? Address { get; set; }
        public required string PartnerNumber { get; set; } 
        public string? CroatianPIN { get; set; }
        public int PartnerTypeId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public required string CreatedByUser { get; set; }
        public bool IsForeign { get; set; }
        public required string ExternalCode { get; set; }
        public int GenderId { get; set; }
    }

}
