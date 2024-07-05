using System.Security.Claims;
using System.Text.Json;
using Domain;

namespace Api.Configurations;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public Guid UserId => Guid.NewGuid();

    public string UserName => _httpContextAccessor?.HttpContext?.User?.Claims
        .FirstOrDefault(x => x.Issuer == ClaimTypes.Name)?.Value ?? "anonymous";

    public Guid TenantId
    {
        get
        {
            var tenantId = _httpContextAccessor?.HttpContext?.Request?.Headers["X-TenantId"];
            Guid.TryParse(tenantId, out Guid tenantIdParsed);
            return tenantIdParsed;
        }
    }

    public List<Tenant> Tenants {
        get
        {
            var tenants = _httpContextAccessor?.HttpContext?.User?.Claims.Where(x => x.Type == "tenants")
                .Select(x => JsonSerializer.Deserialize<Tenant>(x.Value))
                .ToList();
            
            return tenants;
        }
    }
}