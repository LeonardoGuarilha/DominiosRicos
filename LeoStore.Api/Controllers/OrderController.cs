using LeoStore.Domain.StoreContext.Commands.OrderCommands.Inputs;
using LeoStore.Domain.StoreContext.Handlers;
using LeoStore.Domain.StoreContext.Repositories;
using LeoStore.Shared.Commands;
using Microsoft.AspNetCore.Mvc;

namespace LeoStore.Api.Controllers
{
    [Route("v1/orders")]
    public class OrderController
    {
        private readonly IOrderRepository _repository;
        private readonly OrderHandler _handler;
        public OrderController(IOrderRepository repository, OrderHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        [Route("")]
        [HttpPost]
        public ICommandResult Post([FromBody] PlaceOrderCommand command)
        {
            var result = _handler.Handler(command);
            return result;
        }
    }
}