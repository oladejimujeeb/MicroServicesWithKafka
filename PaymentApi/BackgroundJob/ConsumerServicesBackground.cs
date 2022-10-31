using PaymentApi.Interface;

namespace PaymentApi.BackgroundJob
{
    public class ConsumerServicesBackground : BackgroundService
    {

        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ConsumerServicesBackground> _logger;
        public ConsumerServicesBackground(IServiceProvider serviceProvider, ILogger<ConsumerServicesBackground> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var consumerService = _serviceProvider.CreateScope();
                var consume = consumerService.ServiceProvider.GetRequiredService<IStudentDataConsumer>();
                var token = new CancellationTokenSource();
                await consume.ConsumeStudentAsync(token);
                _logger.LogInformation("consuming data");
            }

        }
    }
}
