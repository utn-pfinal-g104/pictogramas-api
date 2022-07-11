using Carter;
using Carter.Response;
using PictogramasApi.Mgmt.Sql.Interface;

namespace PictogramasApi.Modules
{
    public class TagsModule : CarterModule
    {
        private readonly ITagMgmt _tagMgmt;

        public TagsModule(ITagMgmt tagMgmt) : base("/tags")
        {
            _tagMgmt = tagMgmt;

            GetTags();
        }

        private void GetTags()
        {
            Get("/", async (ctx) =>
            {
                var tags = await _tagMgmt.ObtenerTags();
                await ctx.Response.Negotiate(tags);
            });
        }
    }
}
