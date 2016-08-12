using MyCommand;
using Sample.Application.Services;
using Sample.Domain.Model;
using SEOP.AspNet;
using SEOP.Framework.Command;
using SEOP.Framework.Infrastructure;
using SEOP.Framework.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Sample.Controllers
{
    [RoutePrefix("api/values")]
    public class ValuesController : ApiControllerBase
    {
        ProductService _productService;
        IContainer _container;
        ICommandBus _commandBus;
        public ValuesController(ProductService productService, IContainer container, ICommandBus commandBus)
        {
            _productService = productService;
            _container = container;
            _commandBus = commandBus;
        }

        [Route("createProduct")]
        [HttpPost]
        public async Task<ApiResult<Guid>> CreateProduct([FromBody]CreateProduct creatProduct)
        {
            return await ProcessAsync(() => _productService.Handle(creatProduct));
            // if await keyword used in lambda expression, we need add async before () 
            //return await ProcessAsync(async () => await _productService.Handle(creatProduct));
        }

        [Route("reduceProduct")]
        [HttpPost]
        public async Task<ApiResult> ReduceProduct([FromBody]ReduceProduct reduceProduct)
        {
            // command bus processing
            return await ExceptionManager.ProcessAsync(() => _commandBus.ExecuteAsync(reduceProduct));
            // classic processing
            //return await ProcessAsync(() => _productService.Handle(reduceProduct), true);
        }

        [Route("getTopProducts")]
        [HttpGet]
        public async Task<ApiResult<IEnumerable<Product>>> GetTopProducts([FromUri]GetTopProducts getTopProducts)
        {
            return await ProcessAsync(() => _productService.Handle(getTopProducts));
        }

        [Route("getProducts")]
        [HttpGet]
        public ApiResult<IEnumerable<Product>> GetProducts([FromBody]GetProducts getProducts)
        {
            return  Process(() => _productService.Handle(getProducts));
        }
    }
}
