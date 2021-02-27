namespace GrowATree.Application.GuessGame.Queries.GetQuestions
{
    using GrowATree.Application.Models.GuessGameModels;
    using MediatR;

    public class GetQuestionsQuery : IRequest<QuestionListModel>
    {
    }
}
