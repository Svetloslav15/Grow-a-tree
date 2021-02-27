namespace GrowATree.WebAPI.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Common.Constants;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Ml.Queries.PredictLeaf;
    using GrowATree.Application.Models.MlModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Serilog;

    public class MlController : ApiController
    {
        [HttpGet("predict-leaf")]
        [Authorize]
        public async Task<ActionResult<Result<MlViewModel>>> GetList([FromForm] PredictLeafQuery query)
        {
            try
            {
                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<MlViewModel>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }
    }
}
