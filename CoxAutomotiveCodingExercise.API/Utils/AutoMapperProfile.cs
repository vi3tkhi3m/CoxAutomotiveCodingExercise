using AutoMapper;
using CoxAutomotiveCodingExercise.API.Dtos;
using CoxAutomotiveCodingExercise.API.Models;

namespace CoxAutomotiveCodingExercise.API.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<VehicleResponse, Vehicle>();
            CreateMap<DealersResponse, Dealer>();
        }
    }
}
