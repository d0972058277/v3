using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;
using V3Lib.Creationals;
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
        protected IMongoClient _mongoClient;
        protected IMapper _mapper;

        protected IComponentStrategy _componentStrategy;

        public PageController(VisitorFactory visitorBuilderFactory, IMongoClient mongoClient, IMapper mapper)
        {
            _visitorFactory = visitorBuilderFactory;
            _mongoClient = mongoClient;
            _mapper = mapper;

            _componentStrategy = new MongoComponentStrategy(_mongoClient, "Component", "Home", _visitorFactory);
        }
    }
}