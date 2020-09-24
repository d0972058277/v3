using System.Collections.Generic;
using System.Linq;
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
        public class UserHomeCondit
        {
            public bool Hide { get; set; }
            public bool StartDateTime { get; set; }
            public bool EndDateTime { get; set; }
            public List<CommunityStruct> Communities { get; set; } = new List<CommunityStruct>();
            public List<Location> Locations { get; set; } = new List<Location>();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPost("GetUserPage")]
        public async Task<ActionResult<UserPageComponent>> GetUserPage([FromBody] UserHomeCondit condit)
        {
            var configComponent = await _redisComponentStrategy.GetAsync(_cacheHome);

            if (configComponent is null) return Ok(null);

            var conditions = await _configConditsStrategy.GetAsync(_conditionDefined);

            var linkVisitor = _visitorFactory.GetBuilder<LinkComponentRelationVisitorBuilder>().Build();
            configComponent.Accept(linkVisitor);

            var exchangeVisitor = _visitorFactory.GetBuilder<ExchangeRef2DefConditionVisitorBuilder>().SetDefinedConditions(conditions).Build();
            configComponent.Accept(exchangeVisitor);

            var filterVisitorBuilder = _visitorFactory.GetBuilder<FilterComponentVisitorBuilder>();
            if (condit.Hide) filterVisitorBuilder.SetHideFilter();
            if (condit.StartDateTime) filterVisitorBuilder.SetStartDateTimeFilter();
            if (condit.EndDateTime) filterVisitorBuilder.SetEndDateTimeFilter();
            if (condit.Communities.Any()) filterVisitorBuilder.SetCommunityFilter(condit.Communities);
            if (condit.Locations.Any()) filterVisitorBuilder.SetLocationFilter(condit.Locations);
            var filterVisitor = filterVisitorBuilder.Build();
            configComponent.Accept(filterVisitor);

            var removeConditVisitor = _visitorFactory.GetBuilder<RemoveConditionVisitorBuilder>().Build();
            configComponent.Accept(removeConditVisitor);

            var result = _mapper.Map<UserPageComponent>(configComponent);

            return Ok(result);
        }
    }
}