namespace Web.BFF.Models
{
    public record DeviceSummary(string Id, string Name, string Make, string Model, string OS, string AppVersion, DateTime PairedAt, Guid? LicenseId, string? Status);
}
