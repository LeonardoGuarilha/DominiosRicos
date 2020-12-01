using LeoStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using LeoStore.Domain.StoreContext.Handlers;
using LeoStore.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeoStore.Tests.Handlers
{
    [TestClass]
    public class CustomerHandlerTests
    {
        [TestMethod]
        public void ShouldRegisterCustomerWhenCommandIsValid()
        {
            var command = new CreateCustomerCommand();

            command.FirstName = "Leonardo";
            command.LastName = "Guarilha";
            command.Document = "14655803010";
            command.Email = "hello@leo.com";
            command.Phone = "123455436";

            command.Validate();

            var handler = new CustomerHandler(new FakeCustomerRepository(), new FakeEmailService());
            var result = handler.Handler(command);

            Assert.AreNotEqual(null, result);
            Assert.AreEqual(true, handler.Valid);
            Assert.AreEqual(true, command.Valid);
        }
    }
}