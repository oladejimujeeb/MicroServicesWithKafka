using Confluent.Kafka;
using Newtonsoft.Json;
using PaymentApi.Entities;
using PaymentApi.Interface;

namespace PaymentApi.Implementation
{
    public class DataPublisher : IPaymentDetailPublisher
    {
        private readonly IProducer<Null, string> _producer;
     
        private ILogger<DataPublisher> _logger;
        private readonly IConfiguration _configuration;
        public DataPublisher(IProducer<Null, string> producer, ILogger<DataPublisher> logger, IConfiguration configuration)
        {
            _producer = producer;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<bool> ProducePaymentDataAsync(PaymentDetails details)
        {

            try
            {
                var produce = await _producer.ProduceAsync(_configuration["ClientConfigurations:PaymentTopic"],
                                    new Message<Null, string>
                                    {
                                        Value = JsonConvert.SerializeObject(details)
                                    });
                _logger.LogInformation(produce.Value);
                return true;
            }
            catch (ProduceException<Null, string> ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
