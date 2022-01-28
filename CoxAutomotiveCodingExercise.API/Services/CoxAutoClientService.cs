using CoxAutomotiveCodingExercise.API.Models;

namespace CoxAutomotiveCodingExercise.API.Services
{
    public class CoxAutoClientService : ICoxAutoClientService
    {
        public Dealer GetDealerDetails(string dataSetId, int dealerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> GetVehicleIdsFromDataSet(string dataSetId)
        {
            throw new NotImplementedException();
        }

        public VehicleDealer GetVehicleDetails(string dataSetId, int vehicleId)
        {
            throw new NotImplementedException();
        }
    }
}
