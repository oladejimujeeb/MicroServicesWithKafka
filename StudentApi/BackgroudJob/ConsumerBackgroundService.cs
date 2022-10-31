using StudentApi.Interface;

namespace StudentApi.BackgroudJob
{
    public class ConsumerBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ConsumerBackgroundService> _logger;
        public ConsumerBackgroundService(IServiceProvider serviceProvider, ILogger<ConsumerBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var consumerService = _serviceProvider.CreateAsyncScope();
                var consumer = consumerService.ServiceProvider.GetRequiredService<IStudentDataPublisher>();
                await consumer.ConsumeDataAsync();
                _logger.LogInformation("consuming data");
            }
        }
    }
}
