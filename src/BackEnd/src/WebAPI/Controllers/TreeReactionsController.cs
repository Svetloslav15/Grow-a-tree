namespace GrowATree.WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Constants;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.TreeReactions;
    using GrowATree.Application.TreeReactions.Commands.DeleteReaction;
    using GrowATree.Application.TreeReactions.Commands.Upsert;
    using GrowATree.Application.TreeReactions.Queries.GetTreeReactionsByType;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Serilog;

    public class TreeReactionsController : ApiController
    {
        [Authorize]
        [HttpGet("tree-reaction-types")]
        public async Task<ActionResult<Result<ICollection<TreeReactionTypeModel>>>> Upsert([FromQuery] GetTreeReactionsByTypeForTreeCommand upsertCommand)
        {
            try
            {
                return await this.Mediator.Send(upsertCommand);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<ICollection<TreeReactionTypeModel>>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [Authorize]
        [HttpPost("upsert")]
        public async Task<ActionResult<Result<bool>>> Upsert([FromBody] UpsertTreeReactionCommand upsertCommand)
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

                    return Result<bool>.Failure(errorMessage);
                }

                return await this.Mediator.Send(upsertCommand);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<bool>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [Authorize]
        [HttpPost("delete")]
        public async Task<ActionResult<Result<bool>>> Delete([FromBody] DeleteTreeReactionCommand upsertCommand)
        {
            try
            {
                return await this.Mediator.Send(upsertCommand);
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
