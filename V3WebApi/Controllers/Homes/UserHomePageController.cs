using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using V3Lib.Models;
using V3Lib.Models.Components;
using V3Lib.Models.Conditions;
using V3Lib.Visitors;

namespace V3WebApi.Controllers.Pages
{
    public partial class HomeController : ControllerBase
    {
        [MapToApiVersion("3.0-patch0")]
        [HttpGet("User")]
        public async Task<ActionResult<UserPageComponent>> GetHomeUser()
        {
            var configComponent = await _redisComponentStrategy.GetAsync(_cacheHome);

            if (configComponent is null) return Ok(null);

            var conditions = await _configConditsStrategy.GetAsync(_conditionDefined);

            var linkVisitor = _visitorFactory.GetBuilder<LinkRelationVisitorBuilder>().Build();
            configComponent.Accept(linkVisitor);

            var exchangeVisitor = _visitorFactory.GetBuilder<ExchangeRef2DefConditionVisitorBuilder>().SetDefinedConditions(conditions).Build();
            configComponent.Accept(exchangeVisitor);

            var filterVisitor = _visitorFactory.GetBuilder<FilterComponentVisitorBuilder>().SetHideFilter().SetStartDateTimeFilter().SetEndDateTimeFilter().Build();
            configComponent.Accept(filterVisitor);

            var removeConditVisitor = _visitorFactory.GetBuilder<RemoveConditionVisitorBuilder>().Build();
            configComponent.Accept(removeConditVisitor);

            var result = _mapper.Map<UserPageComponent>(configComponent);

            return Ok(result);
        }
    }
}