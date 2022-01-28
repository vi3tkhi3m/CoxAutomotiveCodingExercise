using CoxAutomotiveCodingExercise.API.Models;

namespace CoxAutomotiveCodingExercise.API.Services
{
    public interface ICoxAutoClientService
    {
        public Dealer GetDealerDetails(string dataSetId, int dealerId);
        public IEnumerable<int> GetVehicleIdsFromDataSet(string dataSetId);
        public VehicleDealer GetVehicleDetails(string dataSetId, int vehicleId);
    }
}
