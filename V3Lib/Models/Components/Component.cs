using System;
using MessagePack;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using V3Lib.BsonExtensions;
using V3Lib.Models.Conditions;
using V3Lib.NewtonsoftJsonExtensions;
using V3Lib.Visitors.Abstractions;

namespace V3Lib.Models.Components
{
    // Bson
    [AddBsonKnowTypes]
    // MsgPack
    [MessagePackObject(true)]
    [Union(0, typeof(LazyComponent))]
    [Union(1, typeof(MemberComponent))]
    [Union(2, typeof(ConfigPageComponent))]
    [Union(3, typeof(AdminPageComponent))]
    [Union(4, typeof(UserPageComponent))]
    // Json
    [AddJsonTypeName]
    public abstract class Component : IElement
    {
        protected Component _upperLayerComponent;

        [IgnoreMember]
        [JsonIgnore]
        [BsonIgnoreIfDefault]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public Guid? UpperLayerComponentId { get; set; } = null;

        public Guid ComponentId { get; set; } = Guid.NewGuid();

        public Condition Condition { get; set; }

        public Component GetRoot()
        {
            if (IsRoot()) return this;
            return _upperLayerComponent.GetRoot();
        }

        public bool IsRoot()
        {
            return _upperLayerComponent is null;
        }

        public virtual void Isolate()
        {
            _upperLayerComponent.RemoveLowerLayerComponent(this);
            RemoveUpperLayerComponent();
        }

        public void RemoveUpperLayerComponent()
        {
            SetUpperLayerComponent(null);
        }

        public void SetUpperLayerComponent(Component component)
        {
            _upperLayerComponent = component;
            UpperLayerComponentId = component?.ComponentId;
        }

        public abstract void Accept(IVisitor visitor);
        protected abstract void RemoveLowerLayerComponent(Component component);
    }
}