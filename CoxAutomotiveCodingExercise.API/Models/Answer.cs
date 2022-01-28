namespace CoxAutomotiveCodingExercise.API.Models
{
    public class Answer
    {
        public string DataSetId { get; set; }
        public DataSet DataSet { get; set; }

        public Answer(string dataSetId, DataSet dataSet)
        {
            DataSetId = dataSetId;
            DataSet = dataSet;
        }
    }
}
