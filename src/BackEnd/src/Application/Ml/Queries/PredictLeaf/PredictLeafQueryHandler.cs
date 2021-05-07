namespace GrowATree.Application.Ml.Queries.PredictLeaf
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Common.Services;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Common.Models;
    using GrowATree.Application.Models.MlModels;
    using GrowATree.Domain.Entities;
    using MediatR;

    public class PredictLeafQueryHandler : IRequestHandler<PredictLeafQuery, Result<MlViewModel>>
    {
        private readonly string folderPath = Path.GetFullPath(@"..\..\") + @"DataImages";
        private readonly IApplicationDbContext context;

        public PredictLeafQueryHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Result<MlViewModel>> Handle(PredictLeafQuery request, CancellationToken cancellationToken)
        {
            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(request.Image.FileName);
            var filePath = this.folderPath + "\\" + imageName;

            using var fileStream = new FileStream(filePath, FileMode.Create);
            await request.Image.CopyToAsync(fileStream);
            fileStream.Close();

            ModelInput sampleData = new ModelInput()
            {
                ImageSource = filePath,
            };

            var predictionResult = MlService.Predict(sampleData);
            var maxScore = predictionResult.Score.Max();

            var result = new MlViewModel
            {
                Score = maxScore,
                TreeName = predictionResult.Prediction,
                ClosestResults = predictionResult.ClosestScores,
            };

            if (maxScore < 0.85)
            {
                var unknownFolderName = this.folderPath + @$"\unknown\{imageName}";
                Directory.CreateDirectory(@"\unknown");
                File.Move(filePath, unknownFolderName);
                await this.context.UnknownTrees.AddAsync(new UnknownTrees
                {
                    ClosestResults = result.ClosestResults,
                    TreeName = result.TreeName,
                    Id = imageName,
                });

                await this.context.SaveChangesAsync(CancellationToken.None);
                return Result<MlViewModel>.Failure("Не можeм да определим вида на вашето дърво :(");
            }

            var newFolderName = this.folderPath + @$"\{result.TreeName}\{imageName}";
            File.Move(filePath, newFolderName);
            return Result<MlViewModel>.Success(result);
        }
    }
}
