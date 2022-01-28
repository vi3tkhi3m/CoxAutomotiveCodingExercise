using CoxAutomotiveCodingExercise.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoxAutomotiveCodingExercise.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataSetController : ControllerBase
    {
        private readonly IDataSetService _dataSetService;

        public DataSetController(IDataSetService dataSetService)
        {
            _dataSetService = dataSetService;
        }

        [HttpGet]
        public IActionResult CreateAndSendAnswer()
        {
            try
            {
                var answer = _dataSetService.CreateAnswer();

                var results = _dataSetService.SendAnswer(answer);

                return Ok(results);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }
    }
}
