namespace GrowATree.WebAPI.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Common.Constants;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.Waterings;
    using GrowATree.Application.Waterings.Commands;
    using GrowATree.Application.Waterings.Queries.CanUserWaterTree;
    using GrowATree.Application.Waterings.Queries.TreeWaterings;
    using GrowATree.Application.Waterings.Queries.TreeWateringsCount;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Serilog;

    public class WateringsController : ApiController
    {
        [Authorize]
        [HttpGet("tree-waterings")]
        public async Task<ActionResult<WateringListModel>> GetWateringsForTree([FromQuery] GetTreeWateringsQuery query)
        {
            try
            {
                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return WateringListModel.Failure<WateringListModel>(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [Authorize]
        [HttpGet("tree-waterings-count")]
        public async Task<ActionResult<Result<int>>> TreeWateringsCount([FromQuery] GetTreeWateringsCountQuery query)
        {
            try
            {
                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<int>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [Authorize]
        [HttpGet("can-user-water-tree")]
        public async Task<ActionResult<Result<bool>>> CanUserWaterTree([FromQuery] CanUserWaterTreeQuery query)
        {
            try
            {
                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<bool>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [Authorize]
        [HttpPost("water-tree")]
        public async Task<ActionResult<Result<bool>>> WaterTree([FromBody] WaterTreeCommand command)
        {
            try
            {
                return await this.Mediator.Send(command);
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
