namespace Web.BFF.Models
{
    public record UserProfile(Guid Id, string DisplayName, IEnumerable<TenantValue> Tenants);
}
