namespace GrowATree.Application.TreeReportings.Queries.GetActiveReportTypes
{
    using System.Collections.Generic;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.TreeReporting;
    using MediatR;

    public class GetActiveTreeReportTypesQuery : IRequest<Result<ICollection<TreeReportTypeModel>>>
    {
        public string TreeId { get; set; }
    }
}
