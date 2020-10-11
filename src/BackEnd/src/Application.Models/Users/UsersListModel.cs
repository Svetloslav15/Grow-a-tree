namespace GrowATree.Application.Models.Users
{
    using System.Collections.Generic;
    using GrowATree.Application.Models.Common.Models;

    public class UsersListModel : MetaResult<IList<UserModel>, PaginationMeta>
    {
    }
}
