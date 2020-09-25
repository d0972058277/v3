using System;
using MessagePack.AspNetCoreMvcFormatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Newtonsoft.Json;
using V3Lib;
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
                    c.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    c.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            services.AddApiVersioning(c =>
            {
                c.ReportApiVersions = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SupportApiVersion();

                c.SwaggerDoc("v3.0-patch0", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Components", Version = "v3.0-patch0" });
                c.SwaggerDoc("v3.0-patch1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Components", Version = "v3.0-patch1" });

                c.OperationFilter<RemoveVersionParameterFilter>();
                c.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
                c.EnableAnnotations();
            });
            services.AddSwaggerGenNewtonsoftSupport();

            // MongoDb
            services.AddSingleton<IMongoClient>(sp =>
            {
                var url = new MongoUrl(Environment.GetEnvironmentVariable("MongoConnectionString"));
                var clientSettings = MongoClientSettings.FromUrl(url);

                // clientSettings.SslSettings = new SslSettings
                // {
                //     CheckCertificateRevocation = false
                // };

                // 舊
                //clientSettings.UseSsl = true;
                //clientSettings.VerifySslCertificate = false;

                // 新
                // clientSettings.UseTls = true;
                // clientSettings.AllowInsecureTls = false;

                return new MongoClient(clientSettings);
            });

            // V3
            services.AddV3();
            // V3 分散式快取
            services.AddV3DistributedCache(options =>
            {
                options.Configuration = Environment.GetEnvironmentVariable("RedisConnectionString");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/Error");

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v3.0-patch0/swagger.json", "V3 Docs");
                c.SwaggerEndpoint("/swagger/v3.0-patch1/swagger.json", "V3 Docs");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}