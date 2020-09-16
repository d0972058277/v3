using Newtonsoft.Json;
using V3Lib.Models.Components;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models
{
    public abstract class Additional<T> : IAdditional<T>
    {
        protected Component _relationComponent;

        public virtual void SetRelationComponent(Component component) => _relationComponent = component;

        public virtual Component GetRelationComponent() => _relationComponent;

        public T Clone()
        {
            var json = JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                ContractResolver = new JsonTypeNameContractResolver()
            });

            var obj = (T) JsonConvert.DeserializeObject(json, this.GetType(), new JsonSerializerSettings
            {
                ContractResolver = new JsonTypeNameContractResolver()
            });

            return obj;
        }
    }
}