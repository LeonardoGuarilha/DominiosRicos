using LeoStore.Domain.StoreContext.Services;

namespace LeoStore.Tests.Fakes
{
    public class FakeEmailService : IEmailService
    {
        public void Send(string to, string from, string subject, string body)
        {

        }
    }
}