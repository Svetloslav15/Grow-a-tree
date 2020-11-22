namespace GrowATree.WebAPI.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Constants;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Images.Commands.UploadImage;
    using GrowATree.Application.Models.Images;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Serilog;

    public class ImagesController : ApiController
    {
        [Authorize]
        [HttpPost("upload-image")]
        public async Task<ActionResult<Result<ImageModel>>> UploadImage([FromForm] UploadImageCommand uploadImageCommand)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    var errorMessage = this.ModelState.Values
                        .Where(x => x.ValidationState == ModelValidationState.Invalid)
                        .Select(x => x.Errors)
                        .Select(x => x.FirstOrDefault()?.ErrorMessage)
                        .FirstOrDefault();

                    return Result<ImageModel>.Failure(errorMessage);
                }

                return await this.Mediator.Send(uploadImageCommand);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<ImageModel>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }
    }
}
