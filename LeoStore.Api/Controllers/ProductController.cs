using LeoStore.Domain.StoreContext.Commands.ProductCommands.Inputs;
using LeoStore.Domain.StoreContext.Handlers;
using LeoStore.Domain.StoreContext.Repositories;
using LeoStore.Shared.Commands;
using Microsoft.AspNetCore.Mvc;

namespace LeoStore.Api.Controllers
{

    [Route("v1/products")]
    public class ProductController
    {
        private readonly IProductRepository _repository;
        private readonly CreateProductHandler _handler;
        public ProductController(IProductRepository repository, CreateProductHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }
        [HttpPost]
        [Route("")]
        public ICommandResult Post([FromBody] CreateProductCommand command)
        {
            var result = _handler.Handler(command);
            return result;
        }

    }
}