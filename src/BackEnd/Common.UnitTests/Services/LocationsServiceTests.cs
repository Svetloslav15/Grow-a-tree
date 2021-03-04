using Common.Services;
using NUnit.Framework;

namespace Common.UnitTests.Services
{
    public class LocationsServiceTests
    {
        [Test]
        public void CalculateDistanceBetweenTwoPointsShouldReturnCorrectResult()
        {
            var service = new LocationsService();

            // act
            var result = service.CalculateDistanceBetweenTwoPoints(42.6884337, 23.3189639, 42.68750381944663, 23.318530532558633);

            // assert
            Assert.AreEqual(result < 2000, true);
        }
    }
}
