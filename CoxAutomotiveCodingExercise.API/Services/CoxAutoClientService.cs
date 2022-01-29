using CoxAutomotiveCodingExercise.API.Dtos;
using CoxAutomotiveCodingExercise.API.Models;
using RestSharp;

namespace CoxAutomotiveCodingExercise.API.Services
{
    public class CoxAutoClientService : ICoxAutoClientService
    {
        private readonly ILogger _logger;
        private readonly RestClient _restClient;
        private const string BaseUrl = "http://api.coxauto-interview.com/api";

        public CoxAutoClientService(ILogger<CoxAutoClientService> logger)
        {
            _logger = logger;
            _restClient = new RestClient(BaseUrl);
        }

        public async Task<DataSetIdResponse> CreateDataSet()
        {
            try
            {
                var request = new RestRequest("/DatasetId");

                _logger.LogInformation($"Sending {request.Method} request to {BaseUrl}{request.Resource}");
                var response = await _restClient.ExecuteAsync<DataSetIdResponse>(request);
                _logger.LogInformation($"Successfully got a response from {BaseUrl}{request.Resource}");

                return response.Data;
            }
            catch (Exception e)
            {
                _logger.LogCritical($"Sending {nameof(CreateDataSet)} request failed!");
                throw new Exception("Call to external source failed!", e);
            }
        }

        public async Task<DealersResponse> GetDealerDetails(string dataSetId, int dealerId)
        {
            try
            {
                var request = new RestRequest($"/{dataSetId}/dealers/{dealerId}");

                _logger.LogInformation($"Sending {request.Method} request to {BaseUrl}{request.Resource}");
                var response = await _restClient.ExecuteAsync<DealersResponse>(request);
                _logger.LogInformation($"Successfully got a response from {BaseUrl}{request.Resource}");

                return response.Data;
            }
            catch (Exception e)
            {
                _logger.LogCritical($"Sending {nameof(GetDealerDetails)} request failed!");
                throw new Exception("Call to external source failed!", e);
            }
        }

        public async Task<VehicleIdsResponse> GetVehicleIdsFromDataSetId(string dataSetId)
        {
            try
            {
                var request = new RestRequest($"/{dataSetId}/vehicles")
                    { RequestFormat = DataFormat.Json };

                _logger.LogInformation($"Sending {request.Method} request to {BaseUrl}{request.Resource}");
                var response = await _restClient.ExecuteAsync<VehicleIdsResponse>(request);
                _logger.LogInformation($"Successfully got a response from {BaseUrl}{request.Resource}");

                return response.Data;
            }
            catch (Exception e)
            {
                _logger.LogCritical($"Sending {nameof(GetVehicleIdsFromDataSetId)} request failed!");
                throw new Exception("Call to external source failed!", e);
            }
        }

        public async Task<VehicleResponse> GetVehicleDetails(string dataSetId, int vehicleId)
        {
            try
            {
                var request = new RestRequest($"/{dataSetId}/vehicles/{vehicleId}");

                _logger.LogInformation($"Sending {request.Method} request to {BaseUrl}{request.Resource}");
                var response = await _restClient.ExecuteAsync<VehicleResponse>(request);
                _logger.LogInformation($"Successfully got a response from {BaseUrl}{request.Resource}");

                return response.Data;
            }
            catch (Exception e)
            {
                _logger.LogCritical($"Sending {nameof(GetVehicleDetails)} request failed!");
                throw new Exception("Call to external source failed!", e);
            }
        }

        public async Task<AnswerResponse> SendAnswer(string dataSetId, DataSet dataSet)
        {
            try
            {
                var request = new RestRequest($"/{dataSetId}/answer", Method.Post)
                    .AddJsonBody(dataSet);

                _logger.LogInformation($"Sending {request.Method} request to {BaseUrl}{request.Resource}");
                var response = await _restClient.ExecuteAsync<AnswerResponse>(request);
                _logger.LogInformation($"Successfully got a response from {BaseUrl}{request.Resource}");

                return response.Data;
            }
            catch (Exception e)
            {
                _logger.LogCritical($"Sending {nameof(SendAnswer)} request failed!");
                throw new Exception("Call to external source failed!", e);
            }
        }
    }
}
