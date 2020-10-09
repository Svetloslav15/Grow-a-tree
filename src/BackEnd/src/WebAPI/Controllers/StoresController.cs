namespace GrowATree.WebUI.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Constants;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Stores.Commands.Upsert;
    using GrowATree.WebAPI.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Serilog;

    public class StoresController : ApiController
    {
        [HttpPost("upsert")]
        public async Task<Result<bool>> Upsert([FromBody] UpsertCommand upsertCommand)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    var errorMessage = this.ModelState.Values
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
                return Result<bool>.Failure(ErrorMessages.AccountFailureErrorMessage);
            }
        }
    }
}
