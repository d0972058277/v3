using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using V3Lib;
using V3Lib.Models;
using V3Lib.Models.Conditions;

namespace V3WebApi.Controllers.Conditions
{
    public partial class ConditionController : ControllerBase
    {
        [MapToApiVersion("3.0-patch0")]
        [HttpGet("FakeDefined")]
        public ActionResult<List<MongoPayloadCondition>> Fake([FromQuery] int number)
        {
            var conditions = new List<MongoPayloadCondition>();
            for (int i = 0; i < number; i++)
            {
                var fake = new DefinedCondition().Fake();
                var payload = new MongoPayloadCondition { Key = i.ToString(), Defined = fake };
                conditions.Add(payload);
            }
            return Ok(conditions);
        }
    }
}