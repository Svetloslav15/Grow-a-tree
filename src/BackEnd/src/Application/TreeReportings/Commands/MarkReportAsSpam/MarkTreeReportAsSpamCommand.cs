namespace GrowATree.Application.TreeReportings.Commands.MarkReportAsSpam
{
    using GrowATree.Application.Common.Models;
    using MediatR;

    public class MarkTreeReportAsSpamCommand : IRequest<Result<bool>>
    {
        public string TreeReportId { get; set; }
    }
}
