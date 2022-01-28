using CoxAutomotiveCodingExercise.API.Models;
using RestSharp;

namespace CoxAutomotiveCodingExercise.API.Services
{
    public class CoxAutoClientService : ICoxAutoClientService
    {
        private readonly RestClient _restClient;
        private readonly string _baseUrl = "http://api.coxauto-interview.com/api";

        public CoxAutoClientService()
        {
            _restClient = new RestClient(_baseUrl);
        }

        public async Task<Dealer> GetDealerDetails(string dataSetId, int dealerId)
        {
            var request = new RestRequest($"/{dataSetId}/dealers/{dealerId}")
                { RequestFormat = DataFormat.Json };

            var response = await _restClient.ExecuteAsync<Dealer>(request);

            return response.Data;
        }

        public async Task<IEnumerable<int>> GetVehicleIdsFromDataSet(string dataSetId)
        {
            var request = new RestRequest($"/{dataSetId}/vehicles")
                { RequestFormat = DataFormat.Json };

            var response = await _restClient.ExecuteAsync<IEnumerable<int>>(request);

            return response.Data;
        }

        public async Task<VehicleDealer> GetVehicleDetails(string dataSetId, int vehicleId)
        {
            var request = new RestRequest($"/{dataSetId}/vehicles/{vehicleId}")
                { RequestFormat = DataFormat.Json };

            var response = await _restClient.ExecuteAsync<VehicleDealer>(request);

            return response.Data;
        }
    }
}
