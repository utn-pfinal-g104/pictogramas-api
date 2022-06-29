using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Carter;
using PictogramasApi.Mgmt;
using PictogramasApi.Mgmt.Impl;
using PictogramasApi.Configuration;

namespace PictogramasApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<DapperContext>();
            services.AddHttpClient();
            services.AddCarter();
            services.AddControllers();
            services.AddSingleton<INeo4JMgmt,Neo4JMgmt>();
            services.AddSingleton<ICategoriaMgmt, CategoriaMgmt>();
            services.AddSingleton<IPictogramaMgmt, PictogramaMgmt>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(builder =>
            {
                builder.MapDefaultControllerRoute();
                builder.MapCarter();
            });
        }
    }
}
