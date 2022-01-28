using AutoMapper;
using CoxAutomotiveCodingExercise.API.Models;

namespace CoxAutomotiveCodingExercise.API.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Dealer, DealerVehicles>();
            CreateMap<VehicleDealer, Vehicle>();
        }
    }
}
