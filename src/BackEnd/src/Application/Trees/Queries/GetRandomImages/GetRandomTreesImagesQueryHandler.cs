﻿namespace GrowATree.Application.Trees.Queries.GetRandomImages
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using global::Common.Constants;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Models.Common.Models;
    using GrowATree.Application.Models.TreeImages;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using MoreLinq;

    public class GetRandomTreesImagesQueryHandler : IRequestHandler<GetRandomTreesImagesQuery, TreeImageListModel>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetRandomTreesImagesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<TreeImageListModel> Handle(GetRandomTreesImagesQuery request, CancellationToken cancellationToken)
        {
            var list = this.context.TreeImages
                .Where(x => x.Tree.IsDeleted == false)
                .OrderBy(x => Guid.NewGuid())
                .Take(Constants.TreeImagesCount)
                .ProjectTo<TreeImageModel>(this.mapper.ConfigurationProvider)
                .DistinctBy(x => x.TreeId)
                .ToList();

            var totalTreesImages = list.Count;
            var meta = new Pagination
            {
                CurrentPage = 1,
                PerPage = totalTreesImages,
                TotalItems = totalTreesImages,
                TotalPages = 1,
            };

            var result = new TreeImageListModel
            {
                Data = list,
                Meta = new PaginationMeta
                {
                    Pagination = meta,
                },
            };

            return result;
        }
    }
}
