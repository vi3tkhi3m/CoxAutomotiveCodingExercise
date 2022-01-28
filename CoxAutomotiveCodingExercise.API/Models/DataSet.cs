namespace CoxAutomotiveCodingExercise.API.Models
{
    public class DataSet
    {
        public List<Dealer> Dealers { get; set; }

        public DataSet()
        {
            Dealers = new List<Dealer>();
        }
    }
}
