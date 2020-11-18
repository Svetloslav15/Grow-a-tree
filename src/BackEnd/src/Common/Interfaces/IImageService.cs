namespace Common.Interfaces
{
    using Microsoft.AspNetCore.Http;

    public interface IImageService
    {
        IFormFile ReadImageFromUrl(string url);
    }
}