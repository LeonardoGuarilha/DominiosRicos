using System;
using Flunt.Notifications;
using LeoStore.Domain.StoreContext.Enums;
using LeoStore.Shared.Commands;

namespace LeoStore.Domain.StoreContext.Commands.CustomerCommands.Inputs
{
    public class AddAddressCommand : Notifiable, ICommand
    {
        public string Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public EAddressType Type { get; set; }

        public void Validate()
        {

        }
    }
}