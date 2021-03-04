using Common.Services;
using GrowATree.Application.Models.MlModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace Common.UnitTests.Services
{
    public class MlServiceTests
    {
        [Test]
        public void PredictShouldReturnResult()
        {
            // act
            var filePath = @"../../../Services/TestImages/validTest.jpg";
            var modelInput = new ModelInput()
            {
                ImageSource = filePath,
            };

            var result = MlService.Predict(modelInput);

            // assert
            Assert.IsNotNull(result);
        }


        [Test]
        public void PredictShouldReturnCorrectResult()
        {
            // act
            var filePath = @"../../../Services/TestImages/Круша.png";
            var modelInput = new ModelInput()
            {
                ImageSource = filePath,
            };

            var result = MlService.Predict(modelInput);

            // assert
            Assert.AreEqual(result.Prediction, "Круша");
            Assert.AreEqual(result.Score.Max() > 0.85, true);
        }
    }
}
