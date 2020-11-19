namespace Common.Interfaces
{
    using Microsoft.AspNetCore.Http;

    public interface IImageService
    {
        byte[] ReadImageFromUrl(string url);
    }
}