﻿namespace CoxAutomotiveCodingExercise.API.Dtos
{
    public class VehicleResponse
    {
        public int VehicleId { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int DealerId { get; set; }
    }
}
