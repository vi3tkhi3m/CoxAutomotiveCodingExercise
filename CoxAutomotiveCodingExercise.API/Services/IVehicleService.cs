using CoxAutomotiveCodingExercise.API.Models;

namespace CoxAutomotiveCodingExercise.API.Services
{
    public interface IVehicleService
    {
        public IEnumerable<int> GetVehicleIdsFromDataSet(string dataSetId);
        public VehicleDealer GetVehicleDetails(string dataSetId, int vehicleId);
    }
}
