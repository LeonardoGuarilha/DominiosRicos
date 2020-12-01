using Flunt.Notifications;
using LeoStore.Domain.StoreContext.Commands.CustomerCommands.Outputs;
using LeoStore.Domain.StoreContext.Commands.ProductCommands.Inputs;
using LeoStore.Domain.StoreContext.Entities;
using LeoStore.Domain.StoreContext.Repositories;
using LeoStore.Shared.Commands;

namespace LeoStore.Domain.StoreContext.Handlers
{
    public class CreateProductHandler :
        Notifiable,
        ICommandHandler<CreateProductCommand>
    {
        private readonly IProductRepository _repository;
        public CreateProductHandler(IProductRepository repository)
        {
            _repository = repository;
        }
        public ICommandResult Handler(CreateProductCommand command)
        {
            if (_repository.CheckProduct(command.Title) != null)
                AddNotification("Product", "Este produto já está cadastrado");

            var product = new Product(command.Title, command.Description, command.Image, command.Price, command.QuantityOnHand);

            AddNotifications(product);

            if (Invalid)
                return new CommandResult(
                    false,
                    "Por favor, corrija os campos abaixo",
                    new
                    {
                        Name = product.Title,
                        Description = product.Description,
                        Price = product.Price
                    }
                );

            _repository.Save(product);

            return new CommandResult(
                true,
                "Produto cadastrado com sucesso",
                new
                {
                    Name = product.Title,
                    Description = product.Description,
                    Price = product.Price
                }
            );
        }
    }
}