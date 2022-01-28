using CoxAutomotiveCodingExercise.API.Dtos;
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

        public async Task<DataSetIdResponse> CreateDataSet()
        {
            var request = new RestRequest($"/datasetId")
                { RequestFormat = DataFormat.Json };

            var response = await _restClient.ExecuteAsync<DataSetIdResponse>(request);

            return response.Data;
        }

        public async Task<DealersResponse> GetDealerDetails(string dataSetId, int dealerId)
        {
            var request = new RestRequest($"/{dataSetId}/dealers/{dealerId}")
                { RequestFormat = DataFormat.Json };

            var response = await _restClient.ExecuteAsync<DealersResponse>(request);

            return response.Data;
        }

        public async Task<VehicleIdsResponse> GetVehicleIdsFromDataSet(string dataSetId)
        {
            var request = new RestRequest($"/{dataSetId}/vehicles")
                { RequestFormat = DataFormat.Json };

            var response = await _restClient.ExecuteAsync<VehicleIdsResponse>(request);

            return response.Data;
        }

        public async Task<VehicleResponse> GetVehicleDetails(string dataSetId, int vehicleId)
        {
            var request = new RestRequest($"/{dataSetId}/vehicles/{vehicleId}")
                { RequestFormat = DataFormat.Json };

            var response = await _restClient.ExecuteAsync<VehicleResponse>(request);

            return response.Data;
        }

        public async Task<AnswerResponse> SendAnswer(string dataSetId, DataSet dataSet)
        {
            var request = new RestRequest($"/{dataSetId}/answer", Method.Post)
                { RequestFormat = DataFormat.Json }
                .AddJsonBody(dataSet);

            var response = await _restClient.ExecuteAsync<AnswerResponse>(request);

            return response.Data;
        }
    }
}
