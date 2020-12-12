namespace GrowATree.WebAPI.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Constants;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.TreePosts;
    using GrowATree.Application.TreePosts.Commands.Delete;
    using GrowATree.Application.TreePosts.Commands.Upsert;
    using GrowATree.Application.TreePosts.Queries.GetList;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Serilog;

    public class TreePostsController : ApiController
    {
        [HttpGet("list")]
        [Authorize]
        public async Task<ActionResult<TreePostListModel>> List([FromQuery] GetTreePostListQuery query)
        {
            try
            {
                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return TreePostListModel.Failure<TreePostListModel>(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [HttpPost("upsert")]
        [Authorize]
        public async Task<ActionResult<Result<bool>>> Upsert([FromBody] UpsertTreePostCommand upsertCommand)
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

        [HttpPost("delete")]
        [Authorize]
        public async Task<ActionResult<Result<bool>>> Delete([FromBody] DeleteTreePostCommand deleteCommand)
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
