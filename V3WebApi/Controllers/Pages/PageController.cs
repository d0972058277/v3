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
    public partial class PageController : ControllerBase
    {
        protected VisitorFactory _visitorFactory;
        protected IMapper _mapper;
        protected MongoComponentStrategy _mongoComponentStrategy;
        protected RedisComponentStrategy _redisComponentStrategy;
        protected MongoConfigConditsStrategy _configConditsStrategy;
        protected MongoComponentHistoryStrategy _historyStrategy;
        protected MongoStrategyParams _componentHome;
        protected MongoStrategyParams _conditionDefined;
        protected MongoStrategyParams _componentHistoryHomeUndo;
        protected MongoStrategyParams _componentHistoryHomeRedo;
        protected RedisStrategyParams _cacheHome;

        public PageController(VisitorFactory visitorBuilderFactory, IMapper mapper, MongoComponentStrategy mongoComponentStrategy, RedisComponentStrategy redisComponentStrategy, MongoConfigConditsStrategy configConditsStrategy, MongoComponentHistoryStrategy historyStrategy)
        {
            _visitorFactory = visitorBuilderFactory;
            _mapper = mapper;
            _mongoComponentStrategy = mongoComponentStrategy;
            _redisComponentStrategy = redisComponentStrategy;
            _configConditsStrategy = configConditsStrategy;
            _historyStrategy = historyStrategy;

            _componentHome = new MongoStrategyParams { Database = "Component", Collection = "Home" };
            _conditionDefined = new MongoStrategyParams { Database = "Condition", Collection = "Defined" };
            _componentHistoryHomeUndo = new MongoStrategyParams { Database = "ComponentHistory", Collection = "HomeUndo", StackSize = 10 };
            _componentHistoryHomeRedo = new MongoStrategyParams { Database = "ComponentHistory", Collection = "HomeRedo", StackSize = 10 };
            _cacheHome = new RedisStrategyParams { Cachekey = "Home" };
        }
    }
}