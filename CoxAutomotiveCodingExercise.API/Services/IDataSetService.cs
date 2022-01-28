using CoxAutomotiveCodingExercise.API.Models;

namespace CoxAutomotiveCodingExercise.API.Services
{
    public interface IDataSetService
    {
        public int SendAnswer(DataSet dataSet);
        public DataSet CreateAnswer();
    }
}
