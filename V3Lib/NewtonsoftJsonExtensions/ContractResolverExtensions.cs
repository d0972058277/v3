using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace V3Lib.NewtonsoftJsonExtensions
{
    public static class ContractResolverExtensions
    {
        public static JsonProperty ApplyAddTypeNameAttribute(this JsonProperty jsonProperty)
        {
            if (jsonProperty.TypeNameHandling == null)
            {
                if (jsonProperty.PropertyType.GetCustomAttribute<AddJsonTypeNameAttribute>(false) != null)
                {
                    jsonProperty.TypeNameHandling = TypeNameHandling.All;
                }
            }
            return jsonProperty;
        }

        public static JsonArrayContract ApplyAddTypeNameAttribute(this JsonArrayContract contract)
        {
            if (contract.ItemTypeNameHandling == null)
            {
                if (contract.CollectionItemType.GetCustomAttribute<AddJsonTypeNameAttribute>(false) != null)
                {
                    contract.ItemTypeNameHandling = TypeNameHandling.All;
                }
            }
            return contract;
        }
    }
}