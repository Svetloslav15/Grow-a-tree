namespace GrowATree.Application.Ml.Queries.PredictLeaf
{
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.MlModels;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class PredictLeafQuery : IRequest<Result<MlViewModel>>
    {
        public IFormFile Image { get; set; }
    }
}
