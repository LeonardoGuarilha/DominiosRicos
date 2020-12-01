using System;
using System.Collections.Generic;
using LeoStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using LeoStore.Domain.StoreContext.Commands.CustomerCommands.Outputs;
using LeoStore.Domain.StoreContext.Entities;
using LeoStore.Domain.StoreContext.Handlers;
using LeoStore.Domain.StoreContext.Queries;
using LeoStore.Domain.StoreContext.Repositories;
using LeoStore.Shared.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LeoStore.Api.Controllers
{
    [Route("v1/customers")]
    public class CustomerController : Controller
    {

        // Injecão de dependência
        private readonly ICustomerRepository _repository;
        private readonly CustomerHandler _handler;
        public CustomerController(ICustomerRepository repository, CustomerHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        [HttpGet]
        [Route("")]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 1)] // O cache nada mais é que um parametro no header da requisição, o Cache-Controll. O Duration é em minutos
        // Location = ResponseCacheLocation.Client, armazena o cache no Client, não é necessário usar, pode ser somente o Duration mesmo
        // Sem o Location ele bate aqui no server e armazena aqui
        public IEnumerable<ListCustomerQueryResult> Get()
        {
            return _repository.Get(); // O próprio C# já serializa para JSON
        }

        [HttpGet]
        [Route("{id}")]
        public GetCustomerQueryResult GetById(string id) // Pego o parametro da rota
        {
            return _repository.GetById(id);
        }

        [HttpGet]
        [Route("{id}/orders")]
        public IEnumerable<ListCustomerOrderQueryResult> GetOrders(Guid id) // Pego o parametro da rota
        {
            return _repository.GetOrders(id);
        }

        [HttpPost]
        [Route("")]
        public ICommandResult Post([FromBody] CreateCustomerCommand command)
        {
            // var result = (CreateCustomerCommandResult)_handler.Handler(command);
            var result = _handler.Handler(command);
            return result;
        }

        [HttpPut]
        [Route("{id}")]
        public Customer Put([FromBody] CreateCustomerCommand command, Guid id)
        {
            // var result = (CreateCustomerCommandResult)_handler.Handler(command);

            // if (_handler.Invalid)
            //     return BadRequest(_handler.Notifications);

            return null;

            // É a mesma coisa do Post, só tem que ser feito o handler
        }

        [HttpDelete]
        [Route("{id}")]
        public object Delete(Guid id)
        {
            // var result = (CreateCustomerCommandResult)_handler.Handler(command);

            // if (_handler.Invalid)
            //     return BadRequest(_handler.Notifications);

            return null;

            // É a mesma coisa do Post, só tem que ser feito o handler
        }
    }
}