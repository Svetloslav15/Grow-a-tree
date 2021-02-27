namespace GrowATree.WebAPI.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Common.Constants;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.GuessGame.Commands.GuessQuestion;
    using GrowATree.Application.GuessGame.Queries.GetQuestions;
    using GrowATree.Application.Ml.Queries.PredictLeaf;
    using GrowATree.Application.Models.GuessGameModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Serilog;

    public class GuessGameController : ApiController
    {
        [HttpGet("get-questions")]
        [Authorize]
        public async Task<ActionResult<QuestionListModel>> GetList([FromForm] GetQuestionsQuery query)
        {
            try
            {
                return await this.Mediator.Send(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Debug.WriteLine(ex.Message);
                return QuestionListModel.Failure<QuestionListModel>(ErrorMessages.GeneralSomethingWentWrong);
            }
        }

        [HttpPost("answer-question")]
        [Authorize]
        public async Task<ActionResult<Result<bool>>> Answer([FromBody] GuessQuestionCommand command)
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
