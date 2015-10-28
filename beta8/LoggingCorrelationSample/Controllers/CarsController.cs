using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Logging;

namespace LoggingCorrelationSample
{
    public class CarsContext : IDisposable
    {
        private readonly ILogger _logger;

        public CarsContext(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CarsContext>();
            _logger.LogDebug("Constructing CarsContext");
        }

        public IEnumerable<string> GetCars()
        {
            _logger.LogInformation("Found 3 cars.");

            return new[]
            {
                "Car 1",
                "Car 2",
                "Car 3"
            };
        }

        public void Dispose()
        {
            _logger.LogDebug("Disposing CarsContext");
        }
    }

    public class CarsController : Controller
    {
        private readonly CarsContext _carsContext;

        public CarsController(CarsContext carsContext)
        {
            _carsContext = carsContext;
        }

        [Route("cars")]
        public IActionResult Get()
        {
            var cars = _carsContext.GetCars();
            return Ok(cars);
        }
    }
}