namespace Domain;

public interface IUserContext
{
    Guid UserId { get; }
    string UserName { get; }
    Guid TenantId { get; }
    List<Tenant> Tenants { get; }
}