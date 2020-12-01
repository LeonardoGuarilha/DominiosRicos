using LeoStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeoStore.Tests.Commands
{
    [TestClass]
    public class CreateCustomerCommandTests
    {
        [TestMethod]
        public void ShouldValidateWhenCommandIsValid()
        {
            var command = new CreateCustomerCommand();

            command.FirstName = "Leonardo";
            command.LastName = "Guarilha";
            command.Document = "14655803010";
            command.Email = "hello@leo.com";
            command.Phone = "123455436";

            command.Validate();

            Assert.AreEqual(true, command.Valid);
        }
        
    }
}