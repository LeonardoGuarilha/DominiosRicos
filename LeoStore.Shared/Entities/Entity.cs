using System;
using Flunt.Notifications;

namespace LeoStore.Shared.Entities
{
    public abstract class Entity : Notifiable
    {
        public Entity()
        {
            Id = Guid.NewGuid().ToString().Replace("-", "");
        }

        public String Id { get; private set; }
    }
}