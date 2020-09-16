using System;
using MessagePack.AspNetCoreMvcFormatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using V3Lib.Creationals;
using V3Lib.NewtonsoftJsonExtensions;
using V3WebApi.SwashbuckleExtensions;

namespace V3WebApi
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
            services.AddControllers()
                // Add MsgPack formatters
                .AddMvcOptions(c =>
                {
                    c.OutputFormatters.Add(new MessagePackOutputFormatter(MessagePack.Resolvers.ContractlessStandardResolver.Options));
                    c.InputFormatters.Add(new MessagePackInputFormatter(MessagePack.Resolvers.ContractlessStandardResolver.Options));
                })
                .AddNewtonsoftJson(c =>
                {
                    c.SerializerSettings.ContractResolver = new JsonTypeNameContractResolver();
                });

            services.AddApiVersioning(c =>
            {
                c.ReportApiVersions = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SupportApiVersion();

                c.SwaggerDoc("v3.0-patch0", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Components", Version = "v3.0-patch0" });

                c.OperationFilter<RemoveVersionParameterFilter>();
                c.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
                c.EnableAnnotations();
            });

            // 分散式快取
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Environment.GetEnvironmentVariable("RedisConnectionString");
                options.InstanceName = "FeatureComponent";
            });

            services.AddVisitorBuilderFactory();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v3.0-patch0/swagger.json", "V3 Docs");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}