using SB.Interfaces;
using Microsoft.AspNetCore.Http;

namespace SB.Resources
{
    public class TenantProvider : ITenantProvider
    {
        public TenantProvider(IHttpContextAccessor accessor)
        {
            if (!(accessor.HttpContext?.Request.Headers.TryGetValue("pst", out var bd)).Value) accessor.HttpContext.Abort();
            Singleton.Instance.DbName = bd.ToString();
        }
    }
}
