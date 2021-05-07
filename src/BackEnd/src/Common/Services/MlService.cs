namespace Common.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using Common.Interfaces;
    using GrowATree.Application.Models.MlModels;
    using Microsoft.ML;
    using Microsoft.ML.Data;

    public class MlService
    {
        private static Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictionEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(CreatePredictionEngine);

        public static string MLNetModelPath = Path.GetFullPath(@"..\..\") + @"MLModel.zip";

        public static PredictionEngine<ModelInput, ModelOutput> CreatePredictionEngine()
        {
            // Create new MLContext
            MLContext mlContext = new MLContext();

            // Load model & create prediction engine
            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var modelInputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            return predEngine;
        }

        public static ModelOutput Predict(ModelInput input)
        {
            ModelOutput result = PredictionEngine.Value.Predict(input);

            if (result.Score.Max() < 0.85)
            {
                // Take all predicted labels
                var labelBuffer = new VBuffer<ReadOnlyMemory<char>>();
                PredictionEngine.Value.OutputSchema["Score"].Annotations.GetValue("SlotNames", ref labelBuffer);
                var labels = labelBuffer.DenseValues().Select(x => x.ToString()).ToArray();

                var top4scores = labels.ToDictionary(
                    l => l,
                    l => (decimal)result.Score[Array.IndexOf(labels, l)])
                    .OrderByDescending(kv => kv.Value)
                    .Take(4);

                var listOfLabels = top4scores
                    .Select(x => x.Key);

                result.ClosestScores = string.Join(",", listOfLabels);
            }

            return result;
        }
    }
}
