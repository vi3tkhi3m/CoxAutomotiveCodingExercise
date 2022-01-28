using AutoMapper;
using CoxAutomotiveCodingExercise.API.Exceptions;
using CoxAutomotiveCodingExercise.API.Services;
using Moq;
using NUnit.Framework;

namespace CoxAutomotiveCodingExercise.API.Tests.Services.DataSetServiceTests
{
    public class CreateAnswerTests
    {
        private DataSetService dataSetService;
        private Mock<ICoxAutoClientService> coxAutoClientServiceMock;

        [SetUp]
        public void Setup()
        {
            coxAutoClientServiceMock = new Mock<ICoxAutoClientService>();
            var mapperMock = new Mock<IMapper>();
            dataSetService = new DataSetService(coxAutoClientServiceMock.Object, mapperMock.Object);
        }

        [Test]
        public void Creating_Answer_With_A_DataSetId_That_Is_Null_Or_Empty_Should_Throw_AppException()
        {
            coxAutoClientServiceMock.Setup(m => m.CreateDataSet().Result)
                .Returns("");

            Assert.Throws<AppException>(() => dataSetService.CreateAnswer());
        }
    }
}
