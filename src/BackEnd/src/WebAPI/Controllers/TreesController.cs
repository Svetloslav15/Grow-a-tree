﻿namespace GrowATree.WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Constants;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.TreeImages;
    using GrowATree.Application.Models.Trees;
    using GrowATree.Application.Stores.Queries.GetById;
    using GrowATree.Application.Trees.Commands.AddImage;
    using GrowATree.Application.Trees.Commands.Delete;
    using GrowATree.Application.Trees.Commands.DeleteImage;
    using GrowATree.Application.Trees.Commands.EditImage;
    using GrowATree.Application.Trees.Commands.RestoreImage;
    using GrowATree.Application.Trees.Commands.UpsertCommand;
    using GrowATree.Application.Trees.Queries.GetClosestTrees;
    using GrowATree.Application.Trees.Queries.GetDeletedImages;
    using GrowATree.Application.Trees.Queries.GetList;
    using GrowATree.Application.Trees.Queries.GetListShortInfo;
    using GrowATree.Application.Trees.Queries.GetRandomImages;
    using GrowATree.Application.Trees.Queries.GetRecentTrees;
    using GrowATree.Application.Trees.Queries.GetShortInfoById;
    using GrowATree.Application.Trees.Queries.GetUserTreesShortInfo;
    using GrowATree.Application.Users.Queries.GetTrees;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Serilog;

    public class TreesController : ApiController
    {
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Result<TreeModel>>> GetById(string id)
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

                    return Result<TreeModel>.Failure(errorMessage);
                }

                var query = new GetTreeByIdQuery { Id = id };
                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<TreeModel>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [Authorize]
        [HttpGet("short-info/{id}")]
        public async Task<ActionResult<Result<TreeShortInfoModel>>> GetShortInfoById(string id)
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

                    return Result<TreeShortInfoModel>.Failure(errorMessage);
                }

                var query = new GetTreeShortInfoQuery { Id = id };
                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<TreeShortInfoModel>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<TreeListModel>> GetTrees([FromQuery] GetTreeListQuery query)
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

                    return TreeListModel.Failure<TreeListModel>(errorMessage);
                }

                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return TreeListModel.Failure<TreeListModel>(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [HttpGet("list-short-info")]
        public async Task<ActionResult<TreeListShortInfoModel>> GetTreesShortInfo([FromQuery] GetTreeListShortInfoQuery query)
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

                    return TreeListShortInfoModel.Failure<TreeListShortInfoModel>(errorMessage);
                }

                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return TreeListShortInfoModel.Failure<TreeListShortInfoModel>(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [HttpGet("short-info")]
        public async Task<ActionResult<Result<TreeShortInfoModel>>> GetTreeShortInfo(string id)
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

                    return Result<TreeShortInfoModel>.Failure(errorMessage);
                }

                var query = new GetTreeShortInfoQuery { Id = id };
                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<TreeShortInfoModel>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [HttpGet("random-images")]
        public async Task<ActionResult<TreeImageListModel>> GetTreeDeletedImages([FromQuery] GetRandomTreesImagesQuery query)
        {
            try
            {
                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return TreeImageListModel.Failure<TreeImageListModel>(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [Authorize]
        [HttpGet("deleted-images")]
        public async Task<ActionResult<TreeImageListModel>> GetTreeDeletedImages([FromQuery] GetTreeDeletedImagesQuery query)
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

                    return TreeImageListModel.Failure<TreeImageListModel>(errorMessage);
                }

                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return TreeImageListModel.Failure<TreeImageListModel>(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<ActionResult<TreeListModel>> GetUserTrees([FromQuery] GetUserTreesQuery query)
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

                    return TreeListModel.Failure<TreeListModel>(errorMessage);
                }

                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return TreeListModel.Failure<TreeListModel>(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [Authorize]
        [HttpGet("user/short-info")]
        public async Task<ActionResult<TreeListShortInfoModel>> GetUserTreeShortInfo([FromQuery] GetUserTreesShortInfoQuery query)
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

                    return TreeListShortInfoModel.Failure<TreeListShortInfoModel>(errorMessage);
                }

                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return TreeListShortInfoModel.Failure<TreeListShortInfoModel>(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [HttpGet("closest-trees-short-info")]
        public async Task<ActionResult<TreeListShortInfoModel>> GetClosestTreesShortInfo([FromQuery] GetClosestTreesShortInfoQuery query)
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

                    return TreeListShortInfoModel.Failure<TreeListShortInfoModel>(errorMessage);
                }

                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return TreeListShortInfoModel.Failure<TreeListShortInfoModel>(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        /// <summary>
        /// Creates a new Tree by the given data.
        /// </summary>
        /// <param name="upsertCommand">A tree data which.</param>
        /// <returns>Returns created tree id.</returns>
        [Authorize]
        [HttpPost("upsert")]
        public async Task<ActionResult<Result<string>>> Upsert([FromForm] UpsertTreeCommand upsertCommand)
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
        /// Deletes a tree by the given data.
        /// </summary>
        /// <param name="deleteCommand">A tree data which.</param>
        /// <returns>Returns deleted tree id.</returns>
        [Authorize]
        [HttpPost("delete")]
        public async Task<ActionResult<Result<string>>> Delete([FromBody] DeleteTreeCommand deleteCommand)
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

                return await this.Mediator.Send(deleteCommand);
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
        [Authorize]
        [HttpPost("update-tree-image")]
        public async Task<ActionResult<Result<string>>> EditTreeImage([FromForm] EditTreeImageCommand editTreeImageCommand)
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
        [Authorize]
        [HttpPost("add-tree-images")]
        public async Task<ActionResult<Result<List<string>>>> AddTreeImages([FromForm] AddTreeImagesCommand addTreeImagesCommand)
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

        /// <summary>
        /// Sets the isDeleted propertie in the db to true.
        /// </summary>
        /// <param name="deleteTreeImagesCommand">The id of the wanted to delete item.</param>
        /// <returns>Returns the id of the deleted image.</returns>
        [Authorize]
        [HttpPost("delete-tree-image")]
        public async Task<ActionResult<Result<string>>> DeleteTreeImages([FromBody] DeleteTreeImageCommand deleteTreeImagesCommand)
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

        /// <summary>
        /// Sets the isDeleted propertie in the db to false.
        /// </summary>
        /// <param name="restoreTreeImagesCommand">The restored entity id.</param>
        /// <returns>Returns the id of the restored entity.</returns>
        [Authorize]
        [HttpPost("restore-tree-image")]
        public async Task<ActionResult<Result<string>>> RestoreTreeImages([FromBody] RestoreTreeImageCommand restoreTreeImagesCommand)
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

                return await this.Mediator.Send(restoreTreeImagesCommand);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<string>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [HttpGet("recent-trees")]
        public async Task<ActionResult<TreeListModel>> GetRecentTrees([FromQuery] GetRecentTreesQuery query)
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

                    return TreeListModel.Failure<TreeListModel>(errorMessage);
                }

                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return TreeListModel.Failure<TreeListModel>(ErrorMessages.GeneralSomethingWentWrong);
            }
        }
    }
}