using CoxAutomotiveCodingExercise.API.Models;

namespace CoxAutomotiveCodingExercise.API.Services
{
    public interface IDealerService
    {
        public Dealer GetDealerDetails(string dataSetId, int dealerId);
    }
}
