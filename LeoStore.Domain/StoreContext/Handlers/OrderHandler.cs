using Flunt.Notifications;
using LeoStore.Domain.StoreContext.Commands.CustomerCommands.Outputs;
using LeoStore.Domain.StoreContext.Commands.OrderCommands.Inputs;
using LeoStore.Domain.StoreContext.Entities;
using LeoStore.Domain.StoreContext.Repositories;
using LeoStore.Domain.StoreContext.ValueObjects;
using LeoStore.Shared.Commands;

namespace LeoStore.Domain.StoreContext.Handlers
{
    public class OrderHandler :
        Notifiable,
        ICommandHandler<PlaceOrderCommand>
    {
        private readonly IOrderRepository _repository;
        public OrderHandler(IOrderRepository repository)
        {
            _repository = repository;
        }
        public ICommandResult Handler(PlaceOrderCommand command)
        {
            var customer = _repository.CustomerExists(command.Id);

            if (customer == null)
                AddNotification("Customer", "Este cliente não existe");

            var name = new Name(customer.FirstName, customer.LastName);
            var document = new Document(customer.Document);
            var email = new Email(customer.Email);

            var customerEntity = new Customer(name, document, email, customer.Phone);
            var order = new Order(command.Id);

            AddNotifications(customerEntity, order);

            if (Invalid)
                return new CommandResult(
                 false,
                 "Por favor, corrija os campos abaixo",
                new
                {
                    Name = customer.FirstName,
                    Document = customer.Document,
                    Email = customer.Email,
                }
             );

            var items = command.OrderItems;
            foreach (var item in items)
            {
                var product = _repository.ProductExists(item.Id);
                if (product == null)
                    AddNotification("Product", "Algum produto inserido não existe");

                order.AddItem(product, item.Quantity);
            }

            order.Place();

            _repository.Save(order);

            return new CommandResult(
                true,
                "Bem vindo a Leo Store!",
                new
                {
                    Name = customer.FirstName,
                    Document = customer.Document,
                    Email = customer.Email,
                    Order = order.Number
                }
            );
        }
    }
}