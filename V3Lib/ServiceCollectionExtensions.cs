using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using V3Lib.Creationals;
using V3Lib.Creationals.Abstractions;
using V3Lib.Models.Components;
using V3Lib.Strategies.Abstractions;

namespace V3Lib
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddV3(this IServiceCollection services)
        {
            BsonRegister();

            services.AddAutoMapper();

            services.AddVisitorFactory();

            services.AddStrategy();

            return services;
        }

        private static void BsonRegister()
        {
            // 設定MongoDb的DateTime格式為Local
            BsonSerializer.RegisterSerializer(typeof(DateTime), new DateTimeSerializer(DateTimeKind.Local));

            // 註冊所有掛上 AddBsonKnowTypesAttribute 的抽象類別
            BsonClassMapExtensions.RegisterV3ClassMap();
        }

        private static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddTransient<IMapper>(sp =>
            {
                var mapperConfiguration = new MapperConfiguration(cfg =>
                {
                    // ConfigPageComponent <-> AdminPageComponent
                    cfg.CreateMap<ConfigPageComponent, AdminPageComponent>();
                    // ConfigPageComponent <-> UserPageComponent
                    cfg.CreateMap<ConfigPageComponent, UserPageComponent>();
                });

                return mapperConfiguration.CreateMapper();
            });

            return services;
        }

        private static IServiceCollection AddVisitorFactory(this IServiceCollection services)
        {
            var types = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.DefinedTypes
                    .Where(type => type
                        .GetInterfaces()
                        .Contains(typeof(IVisitorBuilder)) && !type.IsInterface && !type.IsAbstract))
                .ToList();

            foreach (var type in types)
            {
                services.Add(new ServiceDescriptor(type, type, ServiceLifetime.Transient));
                services.Add(new ServiceDescriptor(typeof(IVisitorBuilder), type, ServiceLifetime.Transient));
            }

            services.AddTransient<VisitorFactory>();

            return services;
        }

        private static IServiceCollection AddStrategy(this IServiceCollection services)
        {
            var types = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.DefinedTypes
                    .Where(type => type
                        .GetInterfaces()
                        .Contains(typeof(IStrategy)) && !type.IsInterface && !type.IsAbstract))
                .ToList();

            foreach (var type in types)
            {
                services.Add(new ServiceDescriptor(type, type, ServiceLifetime.Transient));
            }

            return services;
        }
    }
}