namespace GrowATree.Application.TreeReportings.Queries.GetArchivedReportTypes
{
    using System.Collections.Generic;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.TreeReporting;
    using MediatR;

    public class GetArchivedTreeReportTypesQuery : IRequest<Result<ICollection<TreeReportTypeModel>>>
    {
        public string TreeId { get; set; }
    }
}
