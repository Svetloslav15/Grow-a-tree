namespace GrowATree.Application.Trees.Commands.Delete
{
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using MediatR;
    using System.Data.Entity;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteTreeCommandHandler : IRequestHandler<DeleteTreeCommand, Result<string>>
    {
        private readonly IApplicationDbContext context;

        public DeleteTreeCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Result<string>> Handle(DeleteTreeCommand request, CancellationToken cancellationToken)
        {
            var tree = await this.context.Trees.FirstOrDefaultAsync(x => x.Id == request.Id);

            this.context.Trees.Remove(tree);
            await this.context.SaveChangesAsync(CancellationToken.None);

            return Result<string>.Success("success");
        }
    }
}