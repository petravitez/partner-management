namespace PartnerManagement.Modules.Partners.Models;

public class PartnerDetailsDto
{
    public int Id { get; set; }
    public required string Fullname { get; set; }
    public required string PartnerNumber { get; set; }
    public string? CroatianPIN { get; set; }
    public int PartnerTypeId { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public required string CreatedByUser { get; set; }
    public bool IsForeign { get; set; }
    public string Gender { get; set; }
    public bool IsImportant { get; set; }
}
