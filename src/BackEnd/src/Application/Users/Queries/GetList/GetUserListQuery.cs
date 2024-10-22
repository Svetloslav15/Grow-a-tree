﻿namespace GrowATree.Application.Users.Queries.GetAll
{
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.Users;
    using MediatR;

    public class GetUserListQuery : PagedQuery, IRequest<UserListModel>
    {
    }
}
