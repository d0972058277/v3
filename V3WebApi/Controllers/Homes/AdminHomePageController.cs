using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using V3Lib.Models;
using V3Lib.Models.Components;
using V3Lib.Models.Conditions;
using V3Lib.Models.Histories;
using V3Lib.Models.Styles;
using V3Lib.Strategies;
using V3Lib.Strategies.Abstractions;
using V3Lib.Visitors;

namespace V3WebApi.Controllers.Pages
{
    public partial class HomeController : ControllerBase
    {
        [MapToApiVersion("3.0-patch0")]
        [HttpGet("Admin")]
        public async Task<ActionResult<Component>> GetHomeAdmin()
        {
            var configComponent = await _mongoComponentStrategy.GetAsync(_componentHome);
            var adminComponent = _mapper.Map<AdminPageComponent>(configComponent);
            return Ok(adminComponent);
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPost("Admin")]
        public async Task<ActionResult> PostHomeAdmin([FromBody] ConfigPageComponent home)
        {
            await _mongoComponentStrategy.SetAsync(_componentHome, home);
            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPut("Admin")]
        public async Task<ActionResult> PutHomeAdmin([FromBody] ConfigPageComponent home)
        {
            await _mongoComponentStrategy.SetAsync(_componentHome, home);
            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpDelete("Admin")]
        public async Task<ActionResult> DeleteHomeAdmin()
        {
            await _mongoComponentStrategy.RemoveAsync(_componentHome);
            return Ok();

        }

        [MapToApiVersion("3.0-patch0")]
        [HttpGet("Admin/{componentId}")]
        public async Task<ActionResult<Component>> GetHomeAdmin(Guid componentId)
        {
            var configComponent = await _mongoComponentStrategy.GetAsync(_componentHome);

            var flatVisitor = _visitorFactory.GetBuilder<FlatComponentVisitorBuilder>().Build();
            configComponent.Accept(flatVisitor);
            var targetComponent = flatVisitor.FlatElements[componentId];

            return Ok(targetComponent);
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPut("Admin/{componentId}/Lazy")]
        public async Task<ActionResult> PutHomeAdminLazy(Guid componentId, [FromBody] LazyComponent exchangeComponent)
        {
            if (componentId != exchangeComponent.ComponentId)
                throw new ArgumentException($"componentId != component.Id");

            var configComponent = await _mongoComponentStrategy.GetAsync(_componentHome);

            var linkVisitor = _visitorFactory.GetBuilder<LinkComponentRelationVisitorBuilder>().Build();
            exchangeComponent.Accept(linkVisitor);

            var flatVisitor = _visitorFactory.GetBuilder<FlatComponentVisitorBuilder>().Build();
            configComponent.Accept(flatVisitor);
            var targetComponent = flatVisitor.FlatElements[componentId];

            var exchangeVisitor = _visitorFactory.GetBuilder<ExchangeComponentVisitorBuilder>().SetExchangeComponent(targetComponent, exchangeComponent).Build();
            configComponent.Accept(exchangeVisitor);

            await _mongoComponentStrategy.SetAsync(_componentHome, configComponent);

            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPut("Admin/{componentId}/Member")]
        public async Task<ActionResult> PutHomeAdminMember(Guid componentId, [FromBody] MemberComponent exchangeComponent)
        {
            if (componentId != exchangeComponent.ComponentId)
                throw new ArgumentException($"componentId != component.Id");

            var configComponent = await _mongoComponentStrategy.GetAsync(_componentHome);

            var linkVisitor = _visitorFactory.GetBuilder<LinkComponentRelationVisitorBuilder>().Build();
            exchangeComponent.Accept(linkVisitor);

            var flatVisitor = _visitorFactory.GetBuilder<FlatComponentVisitorBuilder>().Build();
            configComponent.Accept(flatVisitor);
            var targetComponent = flatVisitor.FlatElements[componentId];

            var exchangeVisitor = _visitorFactory.GetBuilder<ExchangeComponentVisitorBuilder>().SetExchangeComponent(targetComponent, exchangeComponent).Build();
            configComponent.Accept(exchangeVisitor);

            await _mongoComponentStrategy.SetAsync(_componentHome, configComponent);

            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpDelete("Admin/{componentId}")]
        public async Task<ActionResult> DeleteHomeAdmin(Guid componentId)
        {
            var configComponent = await _mongoComponentStrategy.GetAsync(_componentHome);

            var linkVisitor = _visitorFactory.GetBuilder<LinkComponentRelationVisitorBuilder>().Build();
            configComponent.Accept(linkVisitor);

            var removeVisitor = _visitorFactory.GetBuilder<RemoveComponentVisitorBuilder>().SetComponentId(componentId).Build();
            configComponent.Accept(removeVisitor);

            await _mongoComponentStrategy.SetAsync(_componentHome, configComponent);

            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPost("Admin/{componentId}/SubComponents/Lazy")]
        public async Task<ActionResult> PutHomeAdminSubComponentsLazy(Guid componentId, [FromBody] LazyComponent component)
        {
            if (componentId == component.ComponentId)
                throw new ArgumentException($"componentId == component.Id");

            var configComponent = await _mongoComponentStrategy.GetAsync(_componentHome);

            var flatVisitor = _visitorFactory.GetBuilder<FlatComponentVisitorBuilder>().Build();
            configComponent.Accept(flatVisitor);
            var targetComponent = flatVisitor.FlatElements[componentId];
            if (!(targetComponent is CompositeComponent))
            {
                throw new InvalidOperationException("只有 Composite 支援增加 SubComponent。");
            }
            if (flatVisitor.FlatElements.ContainsKey(component.ComponentId))
            {
                throw new InvalidOperationException("ConfigComponent 已具備 component.ComponentId 請使用新的 component.ComponentId。");
            }

            var linkVisitor = _visitorFactory.GetBuilder<LinkComponentRelationVisitorBuilder>().Build();
            component.Accept(linkVisitor);

            (targetComponent as CompositeComponent).AddLowerLayer(component);

            await _mongoComponentStrategy.SetAsync(_componentHome, configComponent);

            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPost("Admin/{componentId}/SubComponents/Member")]
        public async Task<ActionResult> PutHomeAdminSubComponentsMember(Guid componentId, [FromBody] MemberComponent component)
        {
            if (componentId == component.ComponentId)
                throw new ArgumentException($"componentId == component.Id");

            var configComponent = await _mongoComponentStrategy.GetAsync(_componentHome);

            var flatVisitor = _visitorFactory.GetBuilder<FlatComponentVisitorBuilder>().Build();
            configComponent.Accept(flatVisitor);
            var targetComponent = flatVisitor.FlatElements[componentId];
            if (!(targetComponent is CompositeComponent))
            {
                throw new InvalidOperationException("只有 Composite 支援增加 SubComponent。");
            }
            if (flatVisitor.FlatElements.ContainsKey(component.ComponentId))
            {
                throw new InvalidOperationException("ConfigComponent 已具備 component.ComponentId 請使用新的 component.ComponentId。");
            }

            var linkVisitor = _visitorFactory.GetBuilder<LinkComponentRelationVisitorBuilder>().Build();
            component.Accept(linkVisitor);

            (targetComponent as CompositeComponent).AddLowerLayer(component);

            await _mongoComponentStrategy.SetAsync(_componentHome, configComponent);

            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPost("Admin/Apply")]
        public async Task<ActionResult> PostHomeAdminApply()
        {
            // Condition
            var currentDefinedCondition = await _redisConfigConditsStrategy.GetAsync(_cacheCondit);
            var currentConditionHistory = new ConditionHistory
            {
                Editor = new History.HistoryEditor { Name = "Test" },
                Conditions = currentDefinedCondition
            };
            await _conditionHistoryStrategy.PushAsync(_conditionHistoryHomeUndo, currentConditionHistory);
            await _conditionHistoryStrategy.RemoveAsync(_conditionHistoryHomeRedo);

            var condit = await _mongoConfigConditsStrategy.GetAsync(_conditionDefined);
            await _redisConfigConditsStrategy.SetAsync(_cacheCondit, condit);

            // Component
            var currentHome = await _redisComponentStrategy.GetAsync(_cacheHome);
            var currentComponentHistory = new ComponentHistory
            {
                Editor = new History.HistoryEditor { Name = "Test" },
                Component = currentHome
            };
            await _componentHistoryStrategy.PushAsync(_componentHistoryHomeUndo, currentComponentHistory);
            await _componentHistoryStrategy.RemoveAsync(_componentHistoryHomeRedo);

            var home = await _mongoComponentStrategy.GetAsync(_componentHome);
            await _redisComponentStrategy.SetAsync(_cacheHome, home);
            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPost("Admin/Undo")]
        public async Task<ActionResult> PostHomeAdminUndo()
        {
            // Condition
            var currentDefinedCondition = await _redisConfigConditsStrategy.GetAsync(_cacheCondit);
            var currentConditionHistory = new ConditionHistory
            {
                Editor = new History.HistoryEditor { Name = "Test" },
                Conditions = currentDefinedCondition
            };
            await _conditionHistoryStrategy.PushAsync(_conditionHistoryHomeRedo, currentConditionHistory);

            var condit = await _conditionHistoryStrategy.PopAsync(_conditionHistoryHomeUndo);

            await _redisConfigConditsStrategy.SetAsync(_cacheCondit, condit.Conditions);

            // Component
            var configComponent = await _redisComponentStrategy.GetAsync(_cacheHome);
            var componentHistory = new ComponentHistory
            {
                Editor = new History.HistoryEditor { Name = "Test" },
                Component = configComponent
            };
            await _componentHistoryStrategy.PushAsync(_componentHistoryHomeRedo, componentHistory);

            var home = await _componentHistoryStrategy.PopAsync(_componentHistoryHomeUndo);

            await _redisComponentStrategy.SetAsync(_cacheHome, home.Component);
            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPost("Admin/Redo")]
        public async Task<ActionResult> PostHomeAdminRedo()
        {
            // Condition
            var currentDefinedCondition = await _redisConfigConditsStrategy.GetAsync(_cacheCondit);
            var currentConditionHistory = new ConditionHistory
            {
                Editor = new History.HistoryEditor { Name = "Test" },
                Conditions = currentDefinedCondition
            };
            await _conditionHistoryStrategy.PushAsync(_conditionHistoryHomeUndo, currentConditionHistory);

            var condit = await _conditionHistoryStrategy.PopAsync(_conditionHistoryHomeRedo);

            await _redisConfigConditsStrategy.SetAsync(_cacheCondit, condit.Conditions);

            // Component
            var configComponent = await _redisComponentStrategy.GetAsync(_cacheHome);
            var componentHistory = new ComponentHistory
            {
                Editor = new History.HistoryEditor { Name = "Test" },
                Component = configComponent
            };
            await _componentHistoryStrategy.PushAsync(_componentHistoryHomeUndo, componentHistory);

            var home = await _componentHistoryStrategy.PopAsync(_componentHistoryHomeRedo);

            await _redisComponentStrategy.SetAsync(_cacheHome, home.Component);
            return Ok();
        }
    }
}