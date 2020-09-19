using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using MessagePack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;
using Newtonsoft.Json;
using V3Lib;
using V3Lib.Models;
using V3Lib.Models.Conditions;

namespace V3WebApi.Controllers.Conditions
{
    public partial class ConditionController : ControllerBase
    {
        [MapToApiVersion("3.0-patch0")]
        [HttpGet("FakeDefined")]
        public ActionResult<List<ConfigCondition>> Fake([FromQuery] int number)
        {
            var conditions = new List<ConfigCondition>();
            for (int i = 0; i < number; i++)
            {
                var fake = new DefinedCondition().Fake();
                var config = new ConfigCondition { Key = i.ToString(), Defined = fake };
                conditions.Add(config);
            }
            return Ok(conditions);
        }
    }
}