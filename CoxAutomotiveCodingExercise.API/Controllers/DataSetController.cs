using Microsoft.AspNetCore.Mvc;

namespace CoxAutomotiveCodingExercise.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataSetController : ControllerBase
    {
        [HttpGet]
        public IActionResult CreateAndSendAnswer()
        {

            return Ok();
        }
    }
}
