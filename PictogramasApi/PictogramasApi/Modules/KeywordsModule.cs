using Carter;
using Carter.Response;
using PictogramasApi.Mgmt.Sql.Interface;

namespace PictogramasApi.Modules
{
    public class KeywordsModule : CarterModule
    {
        private readonly IPalabraClaveMgmt _palabraClaveMgmt;

        public KeywordsModule(IPalabraClaveMgmt palabraClaveMgmt) : base("keywords")
        {
            _palabraClaveMgmt = palabraClaveMgmt;

            GetKeywords();
        }

        private void GetKeywords()
        {
            Get("/", async (ctx) =>
            {
                var keywords = await _palabraClaveMgmt.ObtenerKeywords();
                await ctx.Response.Negotiate(keywords);
            });
        }
    }
}
