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
    public partial class PageController : ControllerBase
    {
        [MapToApiVersion("3.0-patch0")]
        [HttpGet("User/Home")]
        public async Task<ActionResult<UserPageComponent>> GetUserPageHome()
        {
            var page = new UserPageComponent();

            // var components = await _distributedCache.GetObjectAsync<List<Component>>(Page.Home.ToString());
            // var conditions = await _distributedCache.GetObjectAsync<Dictionary<string, DefinedCondition>>("Conditions");

            // var linkVisitor = _visitorFactory.GetVisitor<LinkRelationVisitor>();
            // components.ForEach(component => component.Accept(linkVisitor));

            // var exchangeVisitor = _visitorFactory.GetBuilder<ExchangeRef2DefConditionVisitorBuilder>().SetDefinedConditions(conditions).Build();
            // components.ForEach(component => component.Accept(exchangeVisitor));

            // var filterVisitor = _visitorFactory.GetBuilder<FilterComponentVisitorBuilder>().SetHideFilter().SetStartDateTimeFilter().SetEndDateTimeFilter().Build();
            // components.ForEach(component => component.Accept(filterVisitor));

            // var removeConditVisitor = _visitorFactory.GetVisitor<RemoveConditionVisitor>();
            // components.ForEach(component => component.Accept(removeConditVisitor));

            // page.SubComponents = components;

            return Ok(page);
        }
    }
}