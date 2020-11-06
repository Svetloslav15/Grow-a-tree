namespace Common.Interfaces
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public interface ICloudinaryService
    {
        bool IsFileValid(IFormFile photoFile);

        Task<string> UploudAsync(IFormFile file);
    }
}
