using AutoMapper;
using CoxAutomotiveCodingExercise.API.Dtos;
using CoxAutomotiveCodingExercise.API.Exceptions;
using CoxAutomotiveCodingExercise.API.Models;

namespace CoxAutomotiveCodingExercise.API.Services
{
    public class DataSetService : IDataSetService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ICoxAutoClientService _coxAutoClientService;

        public DataSetService(
            ILogger<DataSetService> logger,
            IMapper mapper,
            ICoxAutoClientService coxAutoClientService)
        {
            _logger = logger;
            _mapper = mapper;
            _coxAutoClientService = coxAutoClientService;
        }

        public AnswerResponse SendAnswer(Answer answer)
        {
            return _coxAutoClientService.SendAnswer(answer.DataSetId, answer.DataSet).Result;
        }

        public async Task<Answer> CreateAnswer()
        {
            try
            {
                var dataSetId = _coxAutoClientService.CreateDataSet().Result.DatasetId;
                var dataSet = new DataSet();

                if (!String.IsNullOrEmpty(dataSetId))
                {
                    var vehicleIdsList = _coxAutoClientService.GetVehicleIdsFromDataSet(dataSetId).Result.VehicleIds.ToArray();
                    if (vehicleIdsList.Any())
                    {
                        var vehicleResponsesResult = await ExecuteGetVehicleDetailsAsync(vehicleIdsList, dataSetId);
                        _logger.LogInformation("Successfully executed all GetVehicleDetails tasks.");
                        var dealerResponsesResult = await ExecuteGetDealerDetailsAsync(vehicleResponsesResult, dataSetId);
                        _logger.LogInformation("Successfully executed all GetDealerDetails tasks.");

                        foreach (var dealerResponse in dealerResponsesResult)
                        {
                            AddDealerToDataSet(dealerResponse, dataSet);
                        }

                        foreach (var vehicleResponse in vehicleResponsesResult)
                        {
                            AddVehicleToDealer(vehicleResponse, dataSet);
                        }
                    }
                }
                else
                {
                    throw new AppException("Failed to get a new dataSetId from external source.");
                }

                return new Answer(dataSetId, dataSet);
            }
            catch (Exception e)
            {
                _logger.LogCritical("Failed to create an answer. Error message:" + e.Message);
                throw new AppException("Failed to create an answer. Error message:" + e.Message);
            }
        }

        private void AddVehicleToDealer(VehicleResponse vehicleResponse, DataSet dataSet)
        {
            var vehicle = _mapper.Map<Vehicle>(vehicleResponse);
            dataSet.Dealers.Single(x => x.DealerId == vehicleResponse.DealerId).Vehicles.Add(vehicle);
        }

        private void AddDealerToDataSet(DealersResponse dealerResponse, DataSet dataSet)
        {
            var dealer = _mapper.Map<Dealer>(dealerResponse);
            if (dataSet.Dealers.All(x => x.DealerId != dealerResponse.DealerId))
            {
                dataSet.Dealers.Add(dealer);
            }
        }

        private async Task<DealersResponse[]> ExecuteGetDealerDetailsAsync(VehicleResponse[] vehicleResponsesResult, string dataSetId)
        {
            _logger.LogInformation("Adding GetDealerDetails method to task list.");
            var dealerResponsesTasks = vehicleResponsesResult.Select(vehicleResponse =>
                _coxAutoClientService.GetDealerDetails(dataSetId, vehicleResponse.DealerId));
            return await Task.WhenAll(dealerResponsesTasks);
        }

        private async Task<VehicleResponse[]> ExecuteGetVehicleDetailsAsync(int[] vehicleIdsList, string dataSetId)
        {
            _logger.LogInformation("Adding GetVehicleDetails method to task list.");
            var vehicleResponsesTasks =
                vehicleIdsList.Select(vehicleId => _coxAutoClientService.GetVehicleDetails(dataSetId, vehicleId));
            return await Task.WhenAll(vehicleResponsesTasks);
        }
    }
}
