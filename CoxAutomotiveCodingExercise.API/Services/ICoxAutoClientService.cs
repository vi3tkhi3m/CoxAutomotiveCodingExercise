using CoxAutomotiveCodingExercise.API.Models;

namespace CoxAutomotiveCodingExercise.API.Services
{
    public interface ICoxAutoClientService
    {
        public Task<string> CreateDataSet();
        public Task<Dealer> GetDealerDetails(string dataSetId, int dealerId);
        public Task<IEnumerable<int>> GetVehicleIdsFromDataSet(string dataSetId);
        public Task<VehicleDealer> GetVehicleDetails(string dataSetId, int vehicleId);
    }
}
