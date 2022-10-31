using Microsoft.AspNetCore.Mvc;
using PaymentApi.Interface;

namespace PaymentApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IPaymentDetailPublisher _paymentPublisher;
        private readonly IStudentDataConsumer _consumer;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IStudentDataConsumer consumer, IPaymentDetailPublisher payment)
        {
            _logger = logger;
            _consumer = consumer;
            _paymentPublisher = payment;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpPost("ConsumerDataandMakePayment")]
        public async Task<IActionResult> RunKafKa()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            //this method is suppose to be in the background service
            await _consumer.ConsumeStudentAsync(tokenSource);
            return NoContent();
        }
    }
}