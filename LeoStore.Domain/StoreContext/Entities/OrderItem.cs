using System.Collections.Generic;
using Flunt.Notifications;
using LeoStore.Shared.Entities;

namespace LeoStore.Domain.StoreContext.Entities
{
    // Toda classe que eu quiser validar, eu herdo de Notifiable
    public class OrderItem : Entity
    {
        public OrderItem(Product product, string productId, decimal quantity)
        {
            Product = productId;
            Quantity = quantity;
            Price = product.Price;

            if (product.QuantityOnHand < quantity)
                AddNotification("Quantity", "Produto fora de estoque");

            product.DecreaseQuantity(quantity);
        }

        public string Product { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal Price { get; private set; }
    }
}