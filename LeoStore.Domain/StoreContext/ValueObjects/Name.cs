using Flunt.Notifications;
using Flunt.Validations;

namespace LeoStore.Domain.StoreContext.ValueObjects
{
    public class Name : Notifiable
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(FirstName, 3, "FirstName", "O nome deve conter pelo menos 3 caracteres")
                .HasMaxLen(FirstName, 40, "FirstName", "O nome deve conter no máximos 40 caracteres")
                .HasMinLen(LastName, 3, "LastName", "O sobrenome deve conter pelo menos 3 caracteres")
                .HasMaxLen(LastName, 40, "FirstName", "O sobrenome deve conter no máximos 40 caracteres")
            );
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }


        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

    }
}