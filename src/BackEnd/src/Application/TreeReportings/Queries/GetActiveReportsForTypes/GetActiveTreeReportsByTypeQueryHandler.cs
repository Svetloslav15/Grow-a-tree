﻿namespace GrowATree.Application.TreeReportings.Queries.GetActiveReportsForTypes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.TreeReporting;
    using GrowATree.Application.TreeReportings.Queries.GetReportsForTypes;
    using GrowATree.Domain.Enums;
    using GrowATree.WebAPI.Controllers;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetActiveTreeReportsByTypeQueryHandler : IRequestHandler<GetActiveTreeReportsByTypeQuery, TreeReportListModel>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetActiveTreeReportsByTypeQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<TreeReportListModel> Handle(GetActiveTreeReportsByTypeQuery request, CancellationToken cancellationToken)
        {
            var reports = await this.context.TreeReports
                .Where(x => x.TreeId == request.TreeId && 
                            x.IsActive == true && x.Type == (TreeReportType)Enum.Parse(typeof(TreeReportType), request.ReportType) &&
                            x.IsSpam == false &&
                            x.UserId == request.UserId)
                .ProjectTo<TreeReportModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            var totalReports = reports.Count;
            var meta = new Pagination
            {
                CurrentPage = request.Page,
                PerPage = request.PerPage,
                TotalItems = totalReports,
                TotalPages = Convert.ToInt32(Math.Ceiling(totalReports / Convert.ToDouble(request.PerPage))),
            };

            var result = new TreeReportListModel
            {
                Data = reports,
                Meta = new PaginationMeta
                {
                    Pagination = meta,
                },
            };

            return result;
        }
    }
}
