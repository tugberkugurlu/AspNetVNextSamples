using Microsoft.AspNet.Mvc;

namespace LoggingCorrelationSample
{
	public class CarsController : Controller 
	{
		[Route("cars")]
		public IActionResult Get()
		{
			return Ok(new []
			{
				"Car 1",
				"Car 2",
				"Car 3"
			});
		}
	}
}