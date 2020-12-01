using System;
using System.Collections.Generic;
using Flunt.Notifications;
using Flunt.Validations;
using LeoStore.Shared.Commands;

namespace LeoStore.Domain.StoreContext.Commands.OrderCommands.Inputs
{
    public class PlaceOrderCommand : Notifiable, ICommand
    {
        public PlaceOrderCommand()
        {
            OrderItems = new List<OrderItemCommand>();
        }

        public string Id { get; set; }

        public IList<OrderItemCommand> OrderItems { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .HasLen(Id, 36, "Customer", "Identificador do cliente inválido")
                .IsGreaterThan(OrderItems.Count, 0, "Items", "Nenhum item do pedido foi encontrado")
            );
        }
    }


    public class OrderItemCommand
    {
        public string Id { get; set; }
        public decimal Quantity { get; set; }
    }
}