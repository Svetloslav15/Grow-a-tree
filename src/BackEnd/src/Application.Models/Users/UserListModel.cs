namespace GrowATree.Application.Models.Users
{
    using System.Collections.Generic;
    using GrowATree.Application.Models.Common.Models;

    public class UserListModel : MetaResult<IList<UserModel>, PaginationMeta>
    {
    }
}
