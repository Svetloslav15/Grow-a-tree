namespace GrowATree.Application.Models.TreeReporting
{
    using System.Collections.Generic;
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.WebAPI.Controllers;

    public class TreeReportListModel : MetaResult<IList<TreeReportModel>, PaginationMeta>
    {
    }
}
