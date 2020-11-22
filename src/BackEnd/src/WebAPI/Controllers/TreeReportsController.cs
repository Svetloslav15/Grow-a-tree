namespace GrowATree.WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Constants;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.TreeReporting;
    using GrowATree.Application.TreeReportings.Commands.ArchiveReport;
    using GrowATree.Application.TreeReportings.Commands.MarkReportAsSpam;
    using GrowATree.Application.TreeReportings.Commands.ReportTree;
    using GrowATree.Application.TreeReportings.Queries.GetActiveReportTypes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Serilog;

    public class TreeReportsController : ApiController
    {
        [Authorize]
        [HttpGet("active-report-types")]
        public async Task<ActionResult<Result<ICollection<TreeReportTypeModel>>>> ActiveReportTypes([FromQuery] GetActiveTreeReportTypesQuery query)
        {
            try
            {
                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<ICollection<TreeReportTypeModel>>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [Authorize]
        [HttpPost("report-tree")]
        public async Task<ActionResult<Result<bool>>> ReportTree([FromForm] ReportTreeCommand command)
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

                return await this.Mediator.Send(command);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return Result<bool>.Failure(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [Authorize]
        [HttpPost("mark-as-spam")]
        public async Task<ActionResult<Result<bool>>> MarkReportAsSpam([FromBody] MarkTreeReportAsSpamCommand command)
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

        [Authorize]
        [HttpPost("archive-report")]
        public async Task<ActionResult<Result<bool>>> ArchiveReport([FromBody] ArchiveTreeReportCommand command)
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
