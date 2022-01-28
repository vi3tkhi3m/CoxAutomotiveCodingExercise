using CoxAutomotiveCodingExercise.API.Dtos;
using CoxAutomotiveCodingExercise.API.Models;

namespace CoxAutomotiveCodingExercise.API.Services
{
    public interface ICoxAutoClientService
    {
        public Task<DataSetIdResponse> CreateDataSet();
        public Task<DealersResponse> GetDealerDetails(string dataSetId, int dealerId);
        public Task<VehicleIdsResponse> GetVehicleIdsFromDataSet(string dataSetId);
        public Task<VehicleResponse> GetVehicleDetails(string dataSetId, int vehicleId);
        public Task<AnswerResponse> SendAnswer(string dataSetId, DataSet dataSet);
    }
}
