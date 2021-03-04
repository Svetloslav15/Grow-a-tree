using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Common.Interfaces;
using Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Common.UnitTests.Services
{
    public class CloudinaryServiceTests
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryServiceTests()
        {
            this.cloudinary = new Cloudinary(new Account()
            {
                Cloud = "dummy cloud",
                ApiKey = "dummy key",
                ApiSecret = "dummy sectret",
            });
        }

        [Test]
        public void IsFileValidShouldBeTrue()
        {
            // arrange
            var mock = new Mock<Cloudinary>();
            var service = new CloudinaryService(this.cloudinary);

            // act
            var filePath = @"../../../Services/TestImages/validTest.jpg";
            using var stream = File.OpenRead(filePath);
            var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(filePath))
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpg",
            };
            var result = service.IsFileValid(file);

            // assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsFileValidShouldBeFalse()
        {
            // arrange
            var service = new CloudinaryService(this.cloudinary);

            // act
            var filePath = @"../../../Services/TestImages/invalidTest.zip";
            using var stream = File.OpenRead(filePath);
            var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(filePath))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/zip",
            };
            var result = service.IsFileValid(file);

            // assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsFileValidShouldNotThrow()
        {
            // arrange
            var service = new CloudinaryService(this.cloudinary);

            // assert
            Assert.DoesNotThrow(() => service.IsFileValid(null));
        }

        [Test]
        public void UploudAsyncShouldThrow()
        {
            // arrange
            var result = new ImageUploadResult();

            var service = new CloudinaryService(this.cloudinary);

            // act
            var filePath = @"../../../Services/TestImages/validTest.jpg";
            using var stream = File.OpenRead(filePath);
            var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(filePath))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/zip",
            };

            // assert
            // We know that the code reached the clodinary api so our work is properly done
            Assert.ThrowsAsync<NullReferenceException>(async () => { await service.UploudAsync(file); });
        }

        [Test]
        public async Task UploudAsyncByteShouldThrow()
        {
            // arrange
            var result = new ImageUploadResult();

            var service = new CloudinaryService(this.cloudinary);

            // act
            var filePath = @"../../../Services/TestImages/validTest.jpg";
            var byteFile = await File.ReadAllBytesAsync(filePath);

            // assert
            // We know that the code reached the clodinary api so our work is properly done
            Assert.ThrowsAsync<NullReferenceException>(async () => { await service.UploudAsync(byteFile); });
        }
    }
}
