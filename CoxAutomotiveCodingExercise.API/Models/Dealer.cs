namespace CoxAutomotiveCodingExercise.API.Models
{
    public class Dealer
    {
        public int DealerId { get; set; }
        public string Name { get; set; }
        public List<Vehicle> Vehicles { get; set; }

        public Dealer()
        {
            Vehicles = new List<Vehicle>();
        }
    }
}
