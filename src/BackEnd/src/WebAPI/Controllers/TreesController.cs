namespace GrowATree.WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Constants;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Trees.Commands.AddImage;
    using GrowATree.Application.Trees.Commands.DeleteImage;
    using GrowATree.Application.Trees.Commands.EditImage;
    using GrowATree.Application.Trees.Commands.UpsertCommand;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Serilog;

    public class TreesController : ApiController
    {
        /// <summary>
        /// Creates a new Tree by the given data.
        /// </summary>
        /// <param name="upsertCommand">A tree data which.</param>
        /// <returns>Returns created tree id.</returns>
        [HttpPost("upsert")]
        public async Task<Result<string>> Upsert([FromForm] UpsertTreeCommand upsertCommand)
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

                    return Result<string>.Failure(errorMessage);
                }

                return await this.Mediator.Send(upsertCommand);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<string>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        /// <summary>
        /// Gets an image id and the new image file. Uploads it to cloudinary and returns command result.
        /// </summary>
        /// <param name="editTreeImageCommand">A command with image id and new image file. Both are required.</param>
        /// <returns>A result with data that represents the new image url.</returns>
        [HttpPost("update-tree-image")]
        public async Task<Result<string>> EditTreeImage([FromForm] EditTreeImageCommand editTreeImageCommand)
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

                    return Result<string>.Failure(errorMessage);
                }

                return await this.Mediator.Send(editTreeImageCommand);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<string>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        /// <summary>
        /// Gets image files and uploads them to cloudinary. Images are for a particular tree.
        /// </summary>
        /// <param name="addTreeImagesCommand">A tree id for which the images are and the images files.</param>
        /// <returns>Returns all the added images urls.</returns>
        [HttpPost("add-tree-images")]
        public async Task<Result<List<string>>> AddTreeImages([FromForm] AddTreeImagesCommand addTreeImagesCommand)
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

                    return Result<List<string>>.Failure(errorMessage);
                }

                return await this.Mediator.Send(addTreeImagesCommand);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<List<string>>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [HttpPost("delete-tree-image")]
        public async Task<Result<string>> DeleteTreeImages([FromBody] DeleteTreeImageCommand deleteTreeImagesCommand)
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

                    return Result<string>.Failure(errorMessage);
                }

                return await this.Mediator.Send(deleteTreeImagesCommand);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<string>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }
    }
}
