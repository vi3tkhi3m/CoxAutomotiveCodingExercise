using AutoMapper;
using CoxAutomotiveCodingExercise.API.Services;
using Moq;
using NUnit.Framework;

namespace CoxAutomotiveCodingExercise.API.Tests.Services.DataSetServiceTests
{
    public class CreateAnswerTests
    {
        private DataSetService dataSetService;

        [SetUp]
        public void Setup()
        {
            var coxAutoClientServiceMock = new Mock<ICoxAutoClientService>();
            var mapperMock = new Mock<IMapper>();
            dataSetService = new DataSetService(coxAutoClientServiceMock.Object, mapperMock.Object);
        }

        [Test]
        public void Test()
        {

        }
    }
}
