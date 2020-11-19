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
        /// Read image data from url and convert to bytes
        /// </summary>
        /// <param name="url">Url parameter</param>
        /// <returns>Image in bytes</returns>
        public byte[] ReadImageFromUrl(string url)
        {
            using (var webClient = new WebClient())
            {
                return webClient.DownloadData(url);
            }
        }
    }
}