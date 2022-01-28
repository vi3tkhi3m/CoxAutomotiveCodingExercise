namespace CoxAutomotiveCodingExercise.API.Dtos
{
    public class VehicleIdsResponse
    {
        public IEnumerable<int> VehicleIds { get; set; }

        public VehicleIdsResponse()
        {
            VehicleIds = new List<int>();
        }
    }
}
