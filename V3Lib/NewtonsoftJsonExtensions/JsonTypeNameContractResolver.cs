using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace V3Lib.NewtonsoftJsonExtensions
{
    public class JsonTypeNameContractResolver : DefaultContractResolver
    {
        // As of 7.0.1, Json.NET suggests using a static instance for "stateless" contract resolvers, for performance reasons.
        // http://www.newtonsoft.com/json/help/html/ContractResolver.htm
        // http://www.newtonsoft.com/json/help/html/M_Newtonsoft_Json_Serialization_DefaultContractResolver__ctor_1.htm
        // "Use the parameterless constructor and cache instances of the contract resolver within your application for optimal performance."
        // See also https://stackoverflow.com/questions/33557737/does-json-net-cache-types-serialization-information
        static JsonTypeNameContractResolver instance;

        // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
        static JsonTypeNameContractResolver() { instance = new JsonTypeNameContractResolver(); }

        public static JsonTypeNameContractResolver Instance { get { return instance; } }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            return base.CreateProperty(member, memberSerialization)
                .ApplyAddTypeNameAttribute();
        }

        protected override JsonArrayContract CreateArrayContract(Type objectType)
        {
            return base.CreateArrayContract(objectType)
                .ApplyAddTypeNameAttribute();
        }

        protected override JsonDynamicContract CreateDynamicContract(Type objectType)
        {
            return base.CreateDynamicContract(objectType);
        }
    }
}