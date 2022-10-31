using Confluent.Kafka;
using Newtonsoft.Json;
using PaymentApi.Entities;
using PaymentApi.Interface;

namespace PaymentApi.Implementation
{
    public class DataConsumer : IStudentDataConsumer
    {
        
        private readonly IConsumer<Null, string> _consumer;
        private ILogger<DataConsumer> _logger;
        private readonly IConfiguration _configuration;
        private readonly IPaymentDetailPublisher _publisher;
        public DataConsumer( IConsumer<Null, string> consumer, ILogger<DataConsumer> logger, IConfiguration configuration, IPaymentDetailPublisher publisher)
        {
            
            _consumer = consumer;
            _logger = logger;
            _configuration = configuration;
            _publisher = publisher;
        }
        public async Task ConsumeStudentAsync( CancellationTokenSource cancellationToken)
        {
            _consumer.Subscribe(_configuration["ClientConfigurations:Topic"]);
            try
            {
                while (true)
                {
                    var response = _consumer.Consume(cancellationToken.Token);
                    if (response != null)
                    {
                        var details = JsonConvert.DeserializeObject<StudentDetails>(response.Message.Value);
                        _logger.LogInformation(details.ToString());
                        Random random = new Random();
                        var paymentDetail = new PaymentDetails()
                        {
                            HostelId = random.Next(1, 6),
                            TuitionFees = details.TuitionFees,
                            Department = details.Department,
                            Level = details.Level,
                            MatricNo = details.MatricNo,
                            StudentName = details.StudentName,
                            PaymentStatus = PaymentStatus.Paid,
                            CGPA = details.CGPA,
                            Id = details.Id,
                        };
                       var publish = await _publisher.ProducePaymentDataAsync(paymentDetail);
                        if(!publish)
                        {
                            _logger.LogError("Fail to produce payment details");
                        }
                    }
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
            //finally { _consumer.Close(); }
        }
    }
}

