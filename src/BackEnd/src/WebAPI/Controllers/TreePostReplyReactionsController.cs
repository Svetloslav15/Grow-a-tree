﻿namespace GrowATree.WebAPI.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Constants;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.TreePostReplyReactions;
    using GrowATree.Application.TreePostReplyReactions.Commands.Delete;
    using GrowATree.Application.TreePostReplyReactions.Commands.Upsert;
    using GrowATree.Application.TreePostReplyReactions.Queries.GetList;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Serilog;

    public class TreePostReplyReactionsController : ApiController
    {
        [HttpGet("list")]
        [Authorize]
        public async Task<ActionResult<TreePostReplyReactionListModel>> GetList([FromQuery] GetTreePostReplyReactionListQuery query)
        {
            try
            {
                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return TreePostReplyReactionListModel.Failure<TreePostReplyReactionListModel>(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [HttpPost("upsert")]
        [Authorize]
        public async Task<ActionResult<Result<string>>> Upsert([FromBody] UpsertTreePostReplyReactionCommand upsertCommand)
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

        [HttpPost("delete")]
        [Authorize]
        public async Task<ActionResult<Result<bool>>> Delete([FromBody] DeleteTreePostReplyReactionCommand deleteCommand)
        {
            try
            {
                return await this.Mediator.Send(deleteCommand);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<bool>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }
    }
}
