namespace GrowATree.Application.GuessGame.Queries.GetQuestions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Application.Models.GuessGameModels;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetQuestionsQueryHandler : IRequestHandler<GetQuestionsQuery, QuestionListModel>
    {
        private readonly int questionsCount = 5;
        private readonly string folderPath = Path.GetFullPath(@"..\..\") + @"\DataImages\Unknown\";
        private readonly IApplicationDbContext applicationDbContext;

        public GetQuestionsQueryHandler(IApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<QuestionListModel> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
        {
            var random = new Random();
            var files = Directory.GetFiles(this.folderPath, "*", SearchOption.AllDirectories);
            var filesCount = files.Length;
            var usedIndexes = new List<int>();
            var result = new List<QuestionModel>();
            var itterationsCount = filesCount >= this.questionsCount ? this.questionsCount : filesCount;

            for (int i = 0; i < itterationsCount; i++)
            {
                var currentIndex = random.Next(0, filesCount);
                while (usedIndexes.Contains(currentIndex))
                {
                    currentIndex = random.Next(0, filesCount);
                }

                usedIndexes.Add(currentIndex);
                var currentFile = files[currentIndex];
                var bytes = File.ReadAllBytes(currentFile);
                var file = Convert.ToBase64String(bytes);
                var fileNameOnly = currentFile.Substring(currentFile.LastIndexOf(@"\") + 1);
                var options = await this.applicationDbContext.UnknownTrees
                    .FirstOrDefaultAsync(x => x.Id == fileNameOnly);

                result.Add(new QuestionModel
                {
                    Id = fileNameOnly,
                    LeafImage = file,
                    Options = options.ClosestResults,
                });
            }

            return new QuestionListModel
            {
                Data = result,
                Succeeded = true,
            };
        }
    }
}
