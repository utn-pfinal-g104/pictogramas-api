using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Carter;
using PictogramasApi.Mgmt.Sql.Impl;
using PictogramasApi.Mgmt.Sql.Interface;
using PictogramasApi.Configuration;
using PictogramasApi.Mgmt.NoSql;
using PictogramasApi.Mgmt.CMS;
using PictogramasApi.Jobs;
using PictogramasApi.JobsConfiguration;
using Quartz.Spi;
using Quartz.Impl;
using Quartz;
using AutoMapper;
using PictogramasApi.Utils;
using PictogramasApi.Services;
using Microsoft.AspNetCore.Http.Features;

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
            services.Configure<Neo4JConfig>(config => Configuration.GetSection(Neo4JConfig.Section).Bind(config));
            services.Configure<WebServicesConfig>(config => Configuration.GetSection(WebServicesConfig.Section).Bind(config));

            services.AddSingleton<DapperContext>();
            services.AddHttpClient();
            services.AddCarter();
            services.AddControllers();

            services.Configure<FormOptions>(options =>
            {
                options.KeyLengthLimit = int.MaxValue;
                options.ValueCountLimit = int.MaxValue;
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });

            services.AddSingleton<INeo4JMgmt,Neo4JMgmt>();
            services.AddSingleton<IStorageMgmt, StorageMgmt>();

            services.AddSingleton<ICategoriaMgmt, CategoriaMgmt>();
            services.AddSingleton<IPictogramaMgmt, PictogramaMgmt>();
            services.AddSingleton<IPalabraClaveMgmt, PalabraClaveMgmt>();
            services.AddSingleton<IPictogramaPorCategoriaMgmt, PictogramaPorCategoriaMgmt>();
            services.AddSingleton<IUsuarioMgmt, UsuarioaMgmt>();
            services.AddSingleton<IPizarraMgmt, PizarraMgmt>();
            services.AddSingleton<ICategoriaPorUsuarioMgmt, CategoriaPorUsuarioMgmt>();
            services.AddSingleton<IEstadisticaMgmt, EstadisticaMgmt>();

            services.AddSingleton<ArasaacService>();
            services.AddSingleton<GoogleTranslateService>();

            // Add Quartz services
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            // Add our job
            services.AddSingleton<ActualizacionStorageJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(ActualizacionStorageJob),
                cronExpression: Configuration.GetValue<string>("CronExpression")));

            // Quartz Hosted Service
            services.AddHostedService<QuartzHostedService>();

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseRouting();
            app.UseEndpoints(builder =>
            {                
                builder.MapDefaultControllerRoute();
                builder.MapCarter();
            });
            app.UseHttpsRedirection();
        }
    }
}
