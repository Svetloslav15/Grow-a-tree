namespace GrowATree.Application.Trees.Commands.Delete
{
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using MediatR;
    using System;
    using System.Data.Entity;
    using System.Linq;
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
            try
            {
                var tree = this.context.Trees.FirstOrDefault(x => x.Id == request.Id);

                tree.IsDeleted = true;
                await this.context.SaveChangesAsync(cancellationToken);

                return Result<string>.Success("success");
            }
            catch (Exception exception)
            {
                return Result<string>.Failure(exception.Message);
            }
        }
    }
}