using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoxAutomotiveCodingExercise.API.Dtos;
using CoxAutomotiveCodingExercise.API.Exceptions;
using CoxAutomotiveCodingExercise.API.Models;
using CoxAutomotiveCodingExercise.API.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace CoxAutomotiveCodingExercise.API.Tests.Services.DataSetServiceTests
{
    public class CreateAnswerTests
    {
        private Mock<ILogger<DataSetService>> loggerMock;
        private Mock<IMapper> mapperMock;
        private Mock<ICoxAutoClientService> coxAutoClientServiceMock;
        private DataSetService dataSetService;

        [SetUp]
        public void Setup()
        {
            loggerMock = new Mock<ILogger<DataSetService>>();
            mapperMock = new Mock<IMapper>();
            coxAutoClientServiceMock = new Mock<ICoxAutoClientService>();
            dataSetService = new DataSetService(loggerMock.Object, mapperMock.Object, coxAutoClientServiceMock.Object);
        }

        [Test]
        public void Creating_Answer_With_A_DataSetId_That_Is_Empty_Should_Throw_AppException()
        {
            const string dataSetId = "";
            coxAutoClientServiceMock.Setup(m => m.CreateDataSet().Result)
                .Returns(new DataSetIdResponse()
                {
                    DatasetId = dataSetId
                });

            Assert.ThrowsAsync<AppException>(() => dataSetService.CreateAnswer());
        }

        [Test]
        public void Creating_Answer_With_A_DataSetId_That_Is_Null_Should_Throw_AppException()
        {
            const string dataSetId = null;
            coxAutoClientServiceMock.Setup(m => m.CreateDataSet().Result)
                .Returns(new DataSetIdResponse()
                {
                    DatasetId = dataSetId
                });

            Assert.ThrowsAsync<AppException>(() => dataSetService.CreateAnswer());
        }

        [Test]
        public void Creating_Answer_With_Empty_VehicleIdList_Should_Return_Empty_DataSet()
        {
            const string dataSetId = "mockDataSetId";
            coxAutoClientServiceMock.Setup(m => m.CreateDataSet().Result)
                .Returns(new DataSetIdResponse()
                {
                    DatasetId = dataSetId
                });
            coxAutoClientServiceMock.Setup(m => m.GetVehicleIdsFromDataSet(dataSetId).Result)
                .Returns(new VehicleIdsResponse());

            var result = dataSetService.CreateAnswer();

            Assert.That(result.Result.DataSetId, Is.EqualTo(dataSetId));
            Assert.That(result.Result.DataSet.Dealers.Count, Is.EqualTo(0));
        }

        [Test]
        public void Creating_Answer_Should_Not_Have_Duplicate_Dealers_With_The_Same_Ids_In_List_When_There_Is_Two_Different_VehicleResponses_With_The_Same_Dealer_Ids()
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
                DealerId = dealerId,
                Name = dealerName
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
                    DatasetId = dataSetId
                });
            coxAutoClientServiceMock.Setup(m => m.GetVehicleIdsFromDataSet(dataSetId).Result)
                .Returns(new VehicleIdsResponse()
                {
                    VehicleIds = vehiclesIds
                });
            coxAutoClientServiceMock.Setup(m => m.GetVehicleDetails(dataSetId, vehicleId1).Result)
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

            Assert.That(result.Result.DataSet.Dealers.Count(x => x.DealerId == dealerId), Is.EqualTo(1));
        }

        [Test]
        public void Creating_Answer_Will_Add_Vehicle_To_The_Correct_Dealer_When_There_Are_Multiple_Dealers()
        {
            const string dataSetId = "mockDataSetId";
            const int dealerId1 = 0;
            const string dealerName1 = "dealer 1";
            const int dealerId2 = 1;
            const string dealerName2 = "dealer 2";
            var dealer1 = new Dealer()
            {
                DealerId = dealerId1,
                Name = dealerName1
            };
            var dealer2 = new Dealer()
            {
                DealerId = dealerId2,
                Name = dealerName2
            };
            var dealerResponse1 = new DealersResponse()
            {
                DealerId = dealerId1,
                Name = dealerName1
            };
            var dealerResponse2 = new DealersResponse()
            {
                DealerId = dealerId2,
                Name = dealerName2
            };
            const int vehicleId1 = 0;
            const int vehicleId2 = 1;
            const int vehicleId3 = 2;

            var vehiclesIds = new List<int>
            {
                vehicleId1,
                vehicleId2,
                vehicleId3
            };
            var vehicle1 = new Vehicle()
            {
                VehicleId = vehicleId1
            };
            var vehicle2 = new Vehicle()
            {
                VehicleId = vehicleId2
            };
            var vehicle3 = new Vehicle()
            {
                VehicleId = vehicleId3
            };
            var vehicleResponse1 = new VehicleResponse()
            {
                VehicleId = vehicleId1,
                DealerId = dealerId1
            };

            var vehicleResponse2 = new VehicleResponse()
            {
                VehicleId = vehicleId2,
                DealerId = dealerId1
            };
            var vehicleResponse3 = new VehicleResponse()
            {
                VehicleId = vehicleId3,
                DealerId = dealerId2
            };

            coxAutoClientServiceMock.Setup(m => m.CreateDataSet().Result)
                .Returns(new DataSetIdResponse()
                {
                    DatasetId = dataSetId
                });
            coxAutoClientServiceMock.Setup(m => m.GetVehicleIdsFromDataSet(dataSetId).Result)
                .Returns(new VehicleIdsResponse()
                {
                    VehicleIds = vehiclesIds
                });
            coxAutoClientServiceMock.Setup(m => m.GetVehicleDetails(It.Is<string>(x => x == dataSetId), It.Is<int>(z => z == vehicleId1)).Result)
                .Returns(vehicleResponse1);
            coxAutoClientServiceMock.Setup(m => m.GetVehicleDetails(It.Is<string>(x => x == dataSetId), It.Is<int>(z => z == vehicleId2)).Result)
                .Returns(vehicleResponse2);
            coxAutoClientServiceMock.Setup(m => m.GetVehicleDetails(It.Is<string>(x => x == dataSetId), It.Is<int>(z => z == vehicleId3)).Result)
                .Returns(vehicleResponse3);
            coxAutoClientServiceMock.Setup(m => m.GetDealerDetails(It.Is<string>(x => x == dataSetId), It.Is<int>(x => x == dealerId1)).Result)
                .Returns(dealerResponse1);
            coxAutoClientServiceMock.Setup(m => m.GetDealerDetails(It.Is<string>(x => x == dataSetId), It.Is<int>(x => x == dealerId2)).Result)
                .Returns(dealerResponse2);

            mapperMock.Setup(m => m.Map<Dealer>(It.Is<DealersResponse>(x => x == dealerResponse1)))
                .Returns(dealer1);
            mapperMock.Setup(m => m.Map<Dealer>(It.Is<DealersResponse>(x => x == dealerResponse2)))
                .Returns(dealer2);
            mapperMock.Setup(m => m.Map<Vehicle>(It.Is<VehicleResponse>(x => x == vehicleResponse1)))
                .Returns(vehicle1);
            mapperMock.Setup(m => m.Map<Vehicle>(It.Is<VehicleResponse>(x => x == vehicleResponse2)))
                .Returns(vehicle2);
            mapperMock.Setup(m => m.Map<Vehicle>(It.Is<VehicleResponse>(x => x == vehicleResponse3)))
                .Returns(vehicle3);

            var result = dataSetService.CreateAnswer();

            Assert.That(result.Result.DataSet.Dealers.Single(x => x.DealerId == dealerId1).Vehicles.Count(), Is.EqualTo(2));
            Assert.That(result.Result.DataSet.Dealers.Single(x => x.DealerId == dealerId2).Vehicles.Count(), Is.EqualTo(1));
            Assert.That(result.Result.DataSet.Dealers.Single(x => x.DealerId == dealerId2).Vehicles[0].VehicleId, Is.EqualTo(vehicleId3));
        }
    }
}
