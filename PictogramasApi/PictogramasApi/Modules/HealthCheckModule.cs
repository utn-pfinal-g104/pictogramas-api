using Carter;
using Carter.Response;

namespace PictogramasApi.Modules
{
    public class HealthCheckModule : CarterModule
    {
        public HealthCheckModule()
        {
            Get("/", async (ctx) =>
            {
                await ctx.Response.Negotiate("Healthy");
            });
        }
    }
}
