//using System.Net;
//using System.Text;
//using Domain;

//namespace Api.Middlewares;

//public class CheckTenantPermissionMiddleware(RequestDelegate next, IUserContext userContext)
//{
//    public async Task InvokeAsync(HttpContext httpContext)
//    {
//        var userTenant = userContext.TenantId;
//        var userTenants = userContext.Tenants;
//        if (userTenants.All(x => x.Id != userTenant) && !IsSwagger(httpContext))
//        {
//            httpContext.Response.ContentType = "application/json";
//            httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
//            await httpContext.Response.WriteAsync("{\"success\": false, \"message\": \"Você não tem permissão para acessar está organização\"}", Encoding.UTF8);
//            return;
//        }
//        await next(httpContext);
//    }

//    private bool IsSwagger(HttpContext httpContext)
//    {
//        return httpContext.Request.Path.StartsWithSegments("/swagger");
//    }
//}