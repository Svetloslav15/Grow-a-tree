namespace GrowATree.Application.TreeReportings.Commands.ReportTree
{
    using GrowATree.Application.Common.Models;
    using GrowATree.Domain.Enums;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class ReportTreeCommand : IRequest<Result<bool>>
    {
        public string Message { get; set; }

        public TreeReportType Type { get; set; }

        public IFormFile ImageFile { get; set; }

        public string UserId { get; set; }

        public string TreeId { get; set; }
    }
}
