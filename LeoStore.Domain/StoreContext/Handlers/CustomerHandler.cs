using System;
using Flunt.Notifications;
using LeoStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using LeoStore.Domain.StoreContext.Commands.CustomerCommands.Outputs;
using LeoStore.Domain.StoreContext.Entities;
using LeoStore.Domain.StoreContext.Repositories;
using LeoStore.Domain.StoreContext.Services;
using LeoStore.Domain.StoreContext.ValueObjects;
using LeoStore.Shared.Commands;

namespace LeoStore.Domain.StoreContext.Handlers
{
    public class CustomerHandler :
          Notifiable,
          ICommandHandler<CreateCustomerCommand>
    {
        private readonly ICustomerRepository _repository;
        private readonly IEmailService _emailService;
        public CustomerHandler(ICustomerRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handler(CreateCustomerCommand command)
        {
            if (_repository.CheckDocument(command.Document) != null)
                AddNotification("Document", "Este CPF já está em uso");

            if (_repository.CheckEmail(command.Email) != null)
                AddNotification("Email", "Este E-mail já está cadastrado");

            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document);
            var email = new Email(command.Email);

            var customer = new Customer(name, document, email, command.Phone);

            AddNotifications(name, document, email, customer);

            if (Invalid)
                return new CommandResult(
                    false,
                    "Por favor, corrija os campos abaixo",
                    new
                    {
                        Id = customer.Id,
                        Name = name.ToString(),
                        Email = email.Address
                    }
                );

            _repository.Save(customer);

            _emailService.Send(email.Address, "hello@leo.com", "Bem vindo", "Seja bem vindo a Leo Store!");

            return new CommandResult(
                true,
                "Bem vindo a Leo Store!",
                new
                {
                    Id = customer.Id,
                    Name = name.ToString(),
                    Email = email.Address
                }
            );
        }
    }
}