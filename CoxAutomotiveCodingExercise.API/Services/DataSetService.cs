using AutoMapper;
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

        public int SendAnswer(DataSet dataSet)
        {
            throw new NotImplementedException();
        }

        public DataSet CreateAnswer()
        {
            try
            {
                // Create Dataset
                var dataSetId = _coxAutoClientService.CreateDataSet().Result;

                if (!String.IsNullOrEmpty(dataSetId))
                {
                    var vehicleIds = _coxAutoClientService.GetVehicleIdsFromDataSet(dataSetId).Result;

                    if (vehicleIds.Any())
                    {
                        var dealerVehicles = new List<DealerVehicles>();

                        foreach (var vehicleId in vehicleIds)
                        {
                            var vehicleDetails = _coxAutoClientService.GetVehicleDetails(dataSetId, vehicleId).Result;
                            var vehicle = _mapper.Map<Vehicle>(vehicleDetails);

                            if (!dealerVehicles.Any(x => x.DealerId == vehicleDetails.DealderId))
                            {
                                var dealerDetails = _coxAutoClientService.GetDealerDetails(dataSetId, vehicleDetails.DealderId).Result;
                                var dealerVehicle = _mapper.Map<DealerVehicles>(dealerDetails);
                                dealerVehicle.Vehicles.Add(vehicle);
                                dealerVehicles.Add(dealerVehicle);
                            }
                            else
                            {
                                dealerVehicles.Single(x => x.DealerId == vehicleDetails.DealderId).Vehicles.Add(vehicle);
                            }
                        }
                    }
                }
                else
                {
                    throw new AppException("Failed to get a new dataSetId from external source.");
                }

                return new DataSet();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new AppException();
            }
        }
    }
}
