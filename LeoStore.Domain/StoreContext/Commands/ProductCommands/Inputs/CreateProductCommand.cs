using Flunt.Notifications;
using Flunt.Validations;
using LeoStore.Shared.Commands;

namespace LeoStore.Domain.StoreContext.Commands.ProductCommands.Inputs
{
    public class CreateProductCommand : Notifiable, ICommand
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public decimal QuantityOnHand { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .HasMinLen(Title, 3, "Title", "O título deve conter pelo menos 3 caracteres")
                .HasMinLen(Description, 3, "Description", "A descrição deve conter pelo menos 3 caracteres")
                .AreNotEquals(0, int.MaxValue, "Price", "O preço não pode ser 0")
            );
        }
    }
}