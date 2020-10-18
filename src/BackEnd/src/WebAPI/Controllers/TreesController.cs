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
    using GrowATree.Application.Trees.Commands.EditImage;
    using GrowATree.Application.Trees.Commands.UpsertCommand;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Serilog;

    public class TreesController : ApiController
    {
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
        /// Gets a image id and the new image file. Uploads it to cloudinary and returns command result.
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
    }
}
