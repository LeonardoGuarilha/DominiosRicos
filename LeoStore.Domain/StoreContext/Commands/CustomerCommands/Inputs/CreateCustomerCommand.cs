using Flunt.Notifications;
using Flunt.Validations;
using LeoStore.Shared.Commands;

namespace LeoStore.Domain.StoreContext.Commands.CustomerCommands.Inputs
{
    public class CreateCustomerCommand : Notifiable, ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .HasMinLen(FirstName, 3, "FirstName", "O nome deve conter pelo menos 3 caracteres")
                .HasMaxLen(FirstName, 40, "FirstName", "O nome deve conter no máximos 40 caracteres")
                .HasMinLen(LastName, 3, "LastName", "O sobrenome deve conter pelo menos 3 caracteres")
                .HasMaxLen(LastName, 40, "FirstName", "O sobrenome deve conter no máximos 40 caracteres")
                .IsEmail(Email, "Email", "O e-mail é inválido")
                .HasLen(Document, 11, "Document", "CPF inválido")
            );
        }
    }
}