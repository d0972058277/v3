using System;
using System.Linq;
using MongoDB.Bson.Serialization;
using V3Lib.BsonExtensions;

namespace V3Lib
{
    public static class BsonClassMapExtensions
    {
        public static void RegisterV3ClassMap()
        {
            var rootTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.GetCustomAttributes(typeof(AddBsonKnowTypesAttribute), false).Any() && type.IsClass && type.IsAbstract).ToList();

            foreach (var rootType in rootTypes)
            {
                var subTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => rootType.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract).ToList();

                var bsonClassMap = new BsonClassMap(rootType);
                bsonClassMap.AutoMap();
                bsonClassMap.SetIsRootClass(true);
                subTypes.ForEach(type => bsonClassMap.AddKnownType(type));

                BsonClassMap.RegisterClassMap(bsonClassMap);
            }
        }
    }
}