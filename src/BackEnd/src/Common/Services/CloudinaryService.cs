﻿namespace Common.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Common.Interfaces;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> UploudAsync(IFormFile file)
        {
            string url = " ";
            byte[] fileBytes;
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                fileBytes = stream.ToArray();
            }

            using (var uploadStream = new MemoryStream(fileBytes))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, uploadStream),
                    PublicId = $"GrowATree/{Guid.NewGuid().ToString()}",
                };
                var result = await this.cloudinary.UploadAsync(uploadParams);

                url = result.Url.AbsoluteUri;
            }

            return url;
        }

        public bool IsFileValid(IFormFile photoFile)
        {
            if (photoFile == null)
            {
                return true;
            }

            string[] validTypes = new string[]
            {
                "image/x-png", "image/gif", "image/jpeg", "image/jpg", "image/png", "image/gif", "image/svg", "video/x-msvideo", "video/mp4",
            };

            if (!validTypes.Contains(photoFile.ContentType))
            {
                return false;
            }

            return true;
        }
    }
}
