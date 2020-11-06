namespace GrowATree.Application.Users.Queries.GetById
{
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.Users;
    using MediatR;

    public class GetUserByIdQuery : IRequest<Result<UserModel>>
    {
        public string Id { get; set; }
    }
}
