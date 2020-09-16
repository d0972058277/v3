using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using V3Lib.Models.Components;
using V3Lib.Models.Params;
using V3Lib.Models.Styles;
using V3Lib.NewtonsoftJsonExtensions;
using V3Lib.Visitors;
using Xunit;

namespace V3Test.Components
{
    public class MemberComponentTest
    {
        [Fact]
        public void SetUpperLayerComponent()
        {
            var upperComponent = new MemberComponent();
            var memberComponent = new MemberComponent();
            memberComponent.SetUpperLayerComponent(upperComponent);

            Assert.Equal(upperComponent, memberComponent.GetRoot());

            memberComponent.SetUpperLayerComponent(null);

            Assert.Equal(memberComponent, memberComponent.GetRoot());
        }

        [Fact]
        public void Accept()
        {
            var upperComponent = new MemberComponent();
            var memberComponent = new MemberComponent();
            upperComponent.AddLowerLayer(memberComponent);

            var visitor = new FlatComponentVisitor(new NullParams());
            upperComponent.Accept(visitor);

            Assert.Equal(2, visitor.FlatElement.Count);
        }

        [Fact]
        public void AddLowerLayer()
        {
            var upperComponent = new MemberComponent();
            var memberComponent = new MemberComponent();
            upperComponent.AddLowerLayer(memberComponent);

            Assert.True(upperComponent.SubComponents.Contains(memberComponent));
        }

        [Fact]
        public void ClearLowerLayer()
        {
            var upperComponent = new MemberComponent();
            var memberComponent = new MemberComponent();
            upperComponent.AddLowerLayer(memberComponent);
            upperComponent.ClearLowerLayer();

            Assert.False(upperComponent.SubComponents.Any());
        }

        [Fact]
        public void Isolated()
        {
            var upperComponent = new MemberComponent();
            var memberComponent = new MemberComponent();
            upperComponent.AddLowerLayer(memberComponent);

            Assert.False(memberComponent.Isolated());

            memberComponent.Trim();

            Assert.True(memberComponent.Isolated());
        }

        [Fact]
        public void LinkRelation2Extensions()
        {
            var memberComponent = new MemberComponent();
            var style = new BannerStyle();
            memberComponent.Style = style;

            memberComponent.LinkRelation2Extensions();

            Assert.Equal(memberComponent, style.GetRelationComponent());
        }

        [Fact]
        public void LinkRelation2LowerLayers()
        {
            var upperComponent = new MemberComponent();
            var memberComponent = new MemberComponent();
            upperComponent.AddLowerLayer(memberComponent);

            var list = new List<Component>();
            list.Add(upperComponent);

            var json = JsonConvert.SerializeObject(list, new JsonSerializerSettings
            {
                ContractResolver = new JsonTypeNameContractResolver()
            });
            var objs = JsonConvert.DeserializeObject<List<Component>>(json, new JsonSerializerSettings
            {
                ContractResolver = new JsonTypeNameContractResolver()
            });

            objs.First().LinkRelation2LowerLayers();

            Assert.Equal(objs.First(), (objs.First() as MemberComponent).SubComponents.First().GetRoot());
        }

        [Fact]
        public void RemoveLowerLayer()
        {
            var upperComponent = new MemberComponent();
            var memberComponent = new MemberComponent();
            upperComponent.AddLowerLayer(memberComponent);
            upperComponent.RemoveLowerLayer(memberComponent);

            Assert.Empty(upperComponent.SubComponents);
        }
    }
}