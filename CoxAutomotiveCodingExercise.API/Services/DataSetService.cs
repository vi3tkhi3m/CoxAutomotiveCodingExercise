using AutoMapper;
using CoxAutomotiveCodingExercise.API.Dtos;
using CoxAutomotiveCodingExercise.API.Exceptions;
using CoxAutomotiveCodingExercise.API.Models;

namespace CoxAutomotiveCodingExercise.API.Services
{
    public class DataSetService : IDataSetService
    {
        private readonly ICoxAutoClientService _coxAutoClientService;
        private readonly IMapper _mapper;

        public DataSetService(
            ICoxAutoClientService coxAutoClientService,
            IMapper mapper)
        {
            _coxAutoClientService = coxAutoClientService;
            _mapper = mapper;
        }

        public AnswerResponse SendAnswer(Answer answer)
        {
            return _coxAutoClientService.SendAnswer(answer.DataSetId, answer.DataSet).Result;
        }

        public Answer CreateAnswer()
        {
            try
            {
                var dataSetId = _coxAutoClientService.CreateDataSet().Result.datasetId;

                var dataSet = new DataSet();

                if (!String.IsNullOrEmpty(dataSetId))
                {
                    var vehicleIdsList = _coxAutoClientService.GetVehicleIdsFromDataSet(dataSetId).Result.VehicleIds.ToArray();

                    if (vehicleIdsList.Any())
                    {
                        for (int i = 0; i < vehicleIdsList.Count(); i++)
                        {
                            var vehicleDetails = _coxAutoClientService.GetVehicleDetails(dataSetId, vehicleIdsList[i]).Result;
                            var vehicle = _mapper.Map<Vehicle>(vehicleDetails);

                            if (dataSet.Dealers.All(x => x.DealerId != vehicleDetails.DealerId))
                            {
                                var dealerDetails = _coxAutoClientService.GetDealerDetails(dataSetId, vehicleDetails.DealerId).Result;
                                var dealer = _mapper.Map<Dealer>(dealerDetails);
                                dealer.Vehicles.Add(vehicle);
                                dataSet.Dealers.Add(dealer);
                            }
                            else
                            {
                                dataSet.Dealers.Single(x => x.DealerId == vehicleDetails.DealerId).Vehicles.Add(vehicle);
                            }
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
                Console.WriteLine(e);
                throw new AppException(e.Message);
            }
        }
    }
}
