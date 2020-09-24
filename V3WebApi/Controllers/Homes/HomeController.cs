using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;
using V3Lib.Creationals;
using V3Lib.Models.Params;
using V3Lib.Strategies;
using V3Lib.Strategies.Abstractions;

namespace V3WebApi.Controllers.Pages
{
    [ApiVersion("3.0-patch0")]
    [Produces("application/json", "application/x-msgpack")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public partial class HomeController : ControllerBase
    {
        protected VisitorFactory _visitorFactory;
        protected IMapper _mapper;
        protected MongoComponentStrategy _mongoComponentStrategy;
        protected RedisComponentStrategy _redisComponentStrategy;
        protected MongoConfigConditStrategy _mongoConfigConditsStrategy;
        protected RedisConfigConditStrategy _redisConfigConditsStrategy;
        protected MongoComponentHistoryStrategy _componentHistoryStrategy;
        protected MongoConditionHistoryStrategy _conditionHistoryStrategy;
        protected MongoStrategyParams _componentHome;
        protected MongoStrategyParams _conditionDefined;
        protected MongoStrategyParams _componentHistoryHomeUndo;
        protected MongoStrategyParams _componentHistoryHomeRedo;
        protected MongoStrategyParams _conditionHistoryHomeUndo;
        protected MongoStrategyParams _conditionHistoryHomeRedo;
        protected RedisStrategyParams _cacheHome;
        protected RedisStrategyParams _cacheCondit;

        public HomeController(VisitorFactory visitorBuilderFactory, IMapper mapper, MongoComponentStrategy mongoComponentStrategy, RedisComponentStrategy redisComponentStrategy, MongoConfigConditStrategy mongoConfigConditsStrategy, RedisConfigConditStrategy redisConfigConditStrategy, MongoComponentHistoryStrategy componentHistoryStrategy, MongoConditionHistoryStrategy conditionHistoryStrategy)
        {
            _visitorFactory = visitorBuilderFactory;
            _mapper = mapper;
            _mongoComponentStrategy = mongoComponentStrategy;
            _redisComponentStrategy = redisComponentStrategy;
            _mongoConfigConditsStrategy = mongoConfigConditsStrategy;
            _redisConfigConditsStrategy = redisConfigConditStrategy;
            _componentHistoryStrategy = componentHistoryStrategy;
            _conditionHistoryStrategy = conditionHistoryStrategy;

            _componentHome = new MongoStrategyParams { Database = "Component", Collection = "Home" };
            _conditionDefined = new MongoStrategyParams { Database = "Condition", Collection = "Defined" };
            _componentHistoryHomeUndo = new MongoStrategyParams { Database = "ComponentHistory", Collection = "HomeUndo", StackSize = 10 };
            _componentHistoryHomeRedo = new MongoStrategyParams { Database = "ComponentHistory", Collection = "HomeRedo", StackSize = 10 };
            _conditionHistoryHomeUndo = new MongoStrategyParams { Database = "ConditionHistory", Collection = "DefinedUndo", StackSize = 10 };
            _conditionHistoryHomeRedo = new MongoStrategyParams { Database = "ConditionHistory", Collection = "DefinedRedo", StackSize = 10 };
            _cacheHome = new RedisStrategyParams { Cachekey = "Home" };
            _cacheCondit = new RedisStrategyParams { Cachekey = "DefinedCondition" };
        }
    }
}