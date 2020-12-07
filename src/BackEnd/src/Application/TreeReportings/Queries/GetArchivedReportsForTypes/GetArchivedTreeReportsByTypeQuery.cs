namespace GrowATree.Application.TreeReportings.Queries.GetArchivedReportsForTypes
{
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.TreeReporting;
    using MediatR;

    public class GetArchivedTreeReportsByTypeQuery : PagedQuery, IRequest<TreeReportListModel>
    {
        public string TreeId { get; set; }

        public string ReportType { get; set; }
    }
}
