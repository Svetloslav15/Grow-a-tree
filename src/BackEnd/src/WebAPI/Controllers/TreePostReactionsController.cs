using Common.Constants;
using GrowATree.Application.Common.Models;
using GrowATree.Application.TreePostReactions.Commands.Upsert;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GrowATree.WebAPI.Controllers
{
    public class TreePostReactionsController : ApiController
    {

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
                return Result<string>.Failure(ErrorMessages.AccountFailureErrorMessage);
            }
        }
    }
}
