using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CoxAutomotiveCodingExercise.API.Dtos;
using CoxAutomotiveCodingExercise.API.Exceptions;
using CoxAutomotiveCodingExercise.API.Models;
using CoxAutomotiveCodingExercise.API.Services;
using Moq;
using NUnit.Framework;

namespace CoxAutomotiveCodingExercise.API.Tests.Services.DataSetServiceTests
{
    public class CreateAnswerTests
    {
        private DataSetService dataSetService;
        private Mock<IMapper> mapperMock;
        private Mock<ICoxAutoClientService> coxAutoClientServiceMock;

        [SetUp]
        public void Setup()
        {
            coxAutoClientServiceMock = new Mock<ICoxAutoClientService>();
            mapperMock = new Mock<IMapper>();
            dataSetService = new DataSetService(coxAutoClientServiceMock.Object, mapperMock.Object);
        }

        [Test]
        public void Creating_Answer_With_A_DataSetId_That_Is_Empty_Should_Throw_AppException()
        {
            const string dataSetId = "";
            coxAutoClientServiceMock.Setup(m => m.CreateDataSet().Result)
                .Returns(new DataSetIdResponse()
                {
                    datasetId = dataSetId
                });

            Assert.Throws<AppException>(() => dataSetService.CreateAnswer());
        }

        [Test]
        public void Creating_Answer_With_A_DataSetId_That_Is_Null_Should_Throw_AppException()
        {
            const string dataSetId = null;
            coxAutoClientServiceMock.Setup(m => m.CreateDataSet().Result)
                .Returns(new DataSetIdResponse()
                {
                    datasetId = dataSetId
                });

            Assert.Throws<AppException>(() => dataSetService.CreateAnswer());
        }

        [Test]
        public void Creating_Answer_With_Empty_VehicleIdList_Should_Return_Empty_DataSet()
        {
            const string dataSetId = "mockDataSetId";
            coxAutoClientServiceMock.Setup(m => m.CreateDataSet().Result)
                .Returns(new DataSetIdResponse()
                {
                    datasetId = dataSetId
                });
            coxAutoClientServiceMock.Setup(m => m.GetVehicleIdsFromDataSet(dataSetId).Result)
                .Returns(new VehicleIdsResponse());

            var result = dataSetService.CreateAnswer();

            Assert.That(result.DataSetId, Is.EqualTo(dataSetId));
            Assert.That(result.DataSet.Dealers.Count, Is.EqualTo(0));
        }

        [Test]
        public void Creating_Answer_Should_Not_Have_Duplicate_Dealers_With_The_Same_Ids_When_There_Is_Two_Different_VehicleResponses_With_The_Same_Dealer_Ids()
        {
            const string dataSetId = "mockDataSetId";
            const int dealerId = 0;
            const string dealerName = "dealer 1";
            var dealer = new Dealer()
            {
                DealerId = dealerId,
                Name = dealerName
            };
            var dealerResponse = new DealersResponse()
            {
                dealerId = dealerId,
                name = dealerName
            };
            const int vehicleId1 = 0;
            const int vehicleYear1 = 2020;
            const string vehicleMake1 = "Honda";
            const string vehicleModel1 = "Civic";
            const int vehicleId2 = 1;
            const int vehicleYear2 = 2008;
            const string vehicleMake2 = "Honda";
            const string vehicleModel2 = "CRV";
            var vehiclesIds = new List<int>
            {
                vehicleId1,
                vehicleId2
            };
            var vehicle1 = new Vehicle()
            {
                VehicleId = vehicleId1,
                Year = vehicleYear1,
                Make = vehicleMake1,
                Model = vehicleModel1
            };
            var vehicle2 = new Vehicle()
            {
                VehicleId = vehicleId2,
                Year = vehicleYear2,
                Make = vehicleMake2,
                Model = vehicleModel2
            };
            var vehicleResponse1 = new VehicleResponse()
            {
                VehicleId = vehicleId1,
                Year = vehicleYear1,
                Make = vehicleMake1,
                Model = vehicleModel1,
                DealerId = 0
            };

            var vehicleResponse2 = new VehicleResponse()
            {
                VehicleId = vehicleId2,
                Year = vehicleYear2,
                Make = vehicleMake2,
                Model = vehicleModel2,
                DealerId = 0
            };
            coxAutoClientServiceMock.Setup(m => m.CreateDataSet().Result)
                .Returns(new DataSetIdResponse()
                {
                    datasetId = dataSetId
                });
            coxAutoClientServiceMock.Setup(m => m.GetVehicleIdsFromDataSet(dataSetId).Result)
                .Returns(new VehicleIdsResponse()
                {
                    VehicleIds = vehiclesIds
                });
            coxAutoClientServiceMock.SetupSequence(m => m.GetVehicleDetails(dataSetId, vehicleId1).Result)
                .Returns(vehicleResponse1);
            coxAutoClientServiceMock.Setup(m => m.GetVehicleDetails(dataSetId, vehicleId2).Result)
                .Returns(vehicleResponse2);
            coxAutoClientServiceMock.Setup(m => m.GetDealerDetails(dataSetId, dealerId).Result)
                .Returns(dealerResponse);
            mapperMock.Setup(m => m.Map<Vehicle>(vehicleResponse1))
                .Returns(vehicle1);
            mapperMock.Setup(m => m.Map<Vehicle>(vehicleResponse2))
                .Returns(vehicle2);
            mapperMock.Setup(m => m.Map<Dealer>(dealerResponse))
                .Returns(dealer);

            var result = dataSetService.CreateAnswer();

            Assert.That(result.DataSet.Dealers.Count(x => x.DealerId == dealerId), Is.EqualTo(1));
        }
    }
}
