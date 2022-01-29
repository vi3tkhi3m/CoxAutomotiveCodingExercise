using CoxAutomotiveCodingExercise.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoxAutomotiveCodingExercise.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataSetController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IDataSetService _dataSetService;

        public DataSetController(
            ILogger<DataSetController> logger,
            IDataSetService dataSetService)
        {
            _logger = logger;
            _dataSetService = dataSetService;
        }

        [HttpGet]
        public IActionResult CreateAndSendAnswer()
        {
            try
            {
                _logger.LogInformation("Creating an answer.");
                var answer = _dataSetService.CreateAnswer();

                _logger.LogInformation("Sending the answer.");
                var results = _dataSetService.SendAnswer(answer.Result);

                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Something went wrong. Error message {e.Message}. Returning HTTP 400 - Bad Request");
                return BadRequest();
            }
        }
    }
}
