using Common.Services;
using NUnit.Framework;
using System;
using System.Net;

namespace Common.UnitTests.Services
{
    public class ImageServiceTests
    {
        [Test]
        public void PredictShouldReturnResult()
        {
            var service = new ImageService();

            // act
            var result = service.ReadImageFromUrl(@"https://i.redd.it/w3kr4m2fi3111.png");

            // assert
            Assert.GreaterOrEqual(result.Length, 10);
        }

        [Test]
        public void PredictShouldThrow()
        {
            var service = new ImageService();

            // act
            var result = service.ReadImageFromUrl(@"https://i.redd.it/w3kr4m2fi3111.png");

            // assert
            Assert.Throws<WebException>(() => service.ReadImageFromUrl(@"invalidurl"));
        }
    }
}
