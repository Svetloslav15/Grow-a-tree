using Common.Services;
using GrowATree.Domain.Entities;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Common.UnitTests.Services
{
    public class EmailSenderTests
    {
        [Test]
        public async Task SendEmailShouldReturnTrue()
        {
            var service = new EmailSender();

            // act

            var result = await service.SendEmail(new User() { Email = "test@test.test"}, "Test email", "test subject");

            // assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task SendEmailShouldReturnFalse()
        {
            var service = new EmailSender();

            // act
            var result = await service.SendEmail(null, "Test email", "test subject");

            // assert
            Assert.IsFalse(result);
        }

    }
}
