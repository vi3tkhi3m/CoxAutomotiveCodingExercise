using CoxAutomotiveCodingExercise.API.Exceptions;
using CoxAutomotiveCodingExercise.API.Models;

namespace CoxAutomotiveCodingExercise.API.Services
{
    public class DataSetService : IDataSetService
    {
        private readonly ICoxAutoClientService _coxAutoClientService;

        public DataSetService(ICoxAutoClientService coxAutoClientService)
        {
            _coxAutoClientService = coxAutoClientService;
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

                            if (!dealerVehicles.Any(x => x.DealerId == vehicleDetails.DealderId))
                            {
                                var dealerDetails = _coxAutoClientService.GetDealerDetails(dataSetId, vehicleDetails.DealderId).Result;
                                var dealer = new DealerVehicles();
                                dealer.DealerId = dealerDetails.DealerId;
                                dealer.Name = dealerDetails.Name;
                                dealerVehicles.Add(dealer);
                            }
                        }
                    }
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
