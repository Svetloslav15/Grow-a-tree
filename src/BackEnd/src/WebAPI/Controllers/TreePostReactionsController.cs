using Common.Constants;
using GrowATree.Application.Common.Models;
using GrowATree.Application.Models.TreeReactions;
using GrowATree.Application.TreePostReactions.Commands.Upsert;
using GrowATree.Application.TreePostReactions.Queries.GetTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace GrowATree.WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using GrowATree.Application.Models.TreePostReactions;
    using GrowATree.Application.TreePostReactions.Queries.GetList;
    using Serilog;

    public class TreePostReactionsController : ApiController
    {
        [HttpGet("list")]
        [Authorize]
        public async Task<ActionResult<TreePostReactionListModel>> GetList([FromQuery] GetTreePostReactionListQuery query)
        {
            try
            {
                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return TreePostReactionListModel.Failure<TreePostReactionListModel>(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [HttpGet("types")]
        [Authorize]
        public async Task<ActionResult<Result<IList<ReactionTypeModel>>>> GetTypes([FromQuery] GetTreePostReactionTypesQuery query)
        {
            try
            {
                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<IList<ReactionTypeModel>>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [HttpPost("upsert")]
        [Authorize]
        public async Task<ActionResult<Result<string>>> Upsert([FromBody] UpsertTreePostReactionCommand upsertCommand)
        {
            try
            {
                return await this.Mediator.Send(upsertCommand);
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
