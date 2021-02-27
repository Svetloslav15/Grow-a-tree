namespace GrowATree.Application.GuessGame.Commands.GuessQuestion
{
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class GuessQuestionCommand : IRequest<Result<bool>>
    {
        public string QuestionId { get; set; }

        public string Answer { get; set; }
    }
}
