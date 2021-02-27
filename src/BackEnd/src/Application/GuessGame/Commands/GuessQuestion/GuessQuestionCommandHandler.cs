namespace GrowATree.Application.GuessGame.Commands.GuessQuestion
{
    using System.Threading;
    using System.Threading.Tasks;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GuessQuestionCommandHandler : IRequestHandler<GuessQuestionCommand, Result<bool>>
    {
        private readonly IApplicationDbContext context;

        public GuessQuestionCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Result<bool>> Handle(GuessQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await this.context
                .UnknownTrees
                .FirstOrDefaultAsync(x => x.Id == request.QuestionId);

            question.Votes += request.Answer + ",";
            await this.context.SaveChangesAsync(CancellationToken.None);

            return Result<bool>.Success(true);
        }
    }
}
