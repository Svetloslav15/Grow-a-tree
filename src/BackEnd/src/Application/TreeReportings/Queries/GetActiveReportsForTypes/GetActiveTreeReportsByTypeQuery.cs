namespace GrowATree.Application.TreeReportings.Queries.GetReportsForTypes
{
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.TreeReporting;
    using MediatR;

    public class GetActiveTreeReportsByTypeQuery : PagedQuery, IRequest<TreeReportListModel>
    {
        public string TreeId { get; set; }

        public string ReportType { get; set; }
    }
}
