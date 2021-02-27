namespace GrowATree.Application.Models.MlModels
{
    using Microsoft.ML.Data;

    public class ModelOutput
    {
        [ColumnName("PredictedLabel")]
        public string Prediction { get; set; }

        public float[] Score { get; set; }

        public string ClosestScores { get; set; }
    }
}
