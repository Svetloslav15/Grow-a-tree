namespace GrowATree.Application.Trees.Queries.GetDeletedImages
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

    public class GetTreeDeletedImagesQueryHandler : IRequestHandler<GetTreeDeletedImagesQuery, TreeImageListModel>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IIdentityService identityService;

        public GetTreeDeletedImagesQueryHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
        {
            this.context = context;
            this.mapper = mapper;
            this.identityService = identityService;
        }

        public async Task<TreeImageListModel> Handle(GetTreeDeletedImagesQuery request, CancellationToken cancellationToken)
        {

            var list = await this.context.TreeImages
               .Where(x => x.TreeId == request.Id)
               .Skip(request.PerPage * (request.Page - 1))
               .Take(request.PerPage)
               .ProjectTo<TreeImageModel>(this.mapper.ConfigurationProvider)
               .ToListAsync();

            if ((await this.identityService.GetCurrentUserId()) != list.FirstOrDefault().Id)
            {
                return TreeImageListModel.Failure<TreeImageListModel>(ErrorMessages.NotAllowedErrorMessage);
            }

            var totalImages = list.Count;
            var meta = new Pagination
            {
                CurrentPage = request.Page,
                PerPage = request.PerPage,
                TotalItems = totalImages,
                TotalPages = Convert.ToInt32(Math.Ceiling(totalImages / Convert.ToDouble(request.PerPage))),
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
