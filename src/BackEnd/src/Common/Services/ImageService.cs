namespace Common.Services
{
    using System;
    using System.IO;
    using System.Net;
    using Common.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;

    /// <summary>
    /// Service that process image helper operations
    /// </summary>
    public class ImageService : IImageService
    {
        /// <summary>
        /// Read image data from url and convert to IFormFile
        /// </summary>
        /// <param name="url">Url parameter</param>
        /// <returns>Image in IFormFile</returns>
        public IFormFile ReadImageFromUrl(string url)
        {
            using (var webClient = new WebClient())
            {
                byte[] imageBytes = webClient.DownloadData(url);
                var stream = new MemoryStream(imageBytes);
                return new FormFile(stream, 0, imageBytes.Length, "name", Guid.NewGuid().ToString());
            }
        }
    }
}