using Microsoft.AspNet.Http;

namespace MyNotes.Web.MultiTenancy
{
    public static class MultitenancyHttpContextExtensions
    {
        private const string TenantKey = "saaskit.Tenant";

        public static void SetTenant(this HttpContext context, AppTenant tenant)
        {
            //Ensure.Argument.NotNull(context, nameof(context));
            //Ensure.Argument.NotNull(tenantContext, nameof(tenantContext));

            context.Items[TenantKey] = tenant;
        }

        public static AppTenant GetTenant(this HttpContext context)
        {
            //Ensure.Argument.NotNull(context, nameof(context));

            object tenant;
            if (context.Items.TryGetValue(TenantKey, out tenant))
            {
                return tenant as AppTenant;
            }

            return null;
        }
    }
}
