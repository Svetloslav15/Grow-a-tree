namespace GrowATree.Application.TreeReportings.Commands.ArchiveReport
{
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Enums;
    using MediatR;

    public class ArchiveTreeReportCommand : IRequest<Result<bool>>
    {
        public string TreeId { get; set; }

        public string ReportType { get; set; }
    }
}
