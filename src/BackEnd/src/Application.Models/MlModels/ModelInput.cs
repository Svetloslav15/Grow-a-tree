namespace GrowATree.Application.Models.MlModels
{
    using Microsoft.ML.Data;

    public class ModelInput
    {
        [ColumnName("Label"), LoadColumn(0)]
        public string Label { get; set; }

        [ColumnName("ImageSource"), LoadColumn(1)]
        public string ImageSource { get; set; }
    }
}
