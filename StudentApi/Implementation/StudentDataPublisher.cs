using Confluent.Kafka;
using StudentApi.Entities;
using StudentApi.Interface;
using Newtonsoft.Json;

namespace StudentApi.Implementation
{
    public class StudentDataPublisher : IStudentDataPublisher
    {
        private readonly IProducer<Null, string> _producer;
        private readonly IConsumer<Null, string> _consumer;
        private ILogger<StudentDataPublisher> _logger;
        private readonly IConfiguration _configuration;
        private readonly IStudentRepository _studentRepository;


        public StudentDataPublisher(IProducer<Null, string> producer, ILogger<StudentDataPublisher>logger, IConsumer<Null, string> consumer,
            IConfiguration configuration, IStudentRepository studentRepository)
        {
            _producer = producer;
            _logger = logger;
            _consumer = consumer;
            _configuration = configuration;
            _studentRepository = studentRepository;
        }

        public async Task<bool> ConsumeDataAsync()
        {
            _consumer.Subscribe(_configuration["ClientConfigurations:PaymentTopic"]);
            CancellationTokenSource cancellationToken = new();
            try
            {
                while (true)
                {
                    var response = _consumer.Consume(cancellationToken.Token);
                    if(response != null)
                    {
                        var details = JsonConvert.DeserializeObject<StudentDetails>(response.Message.Value);
                        //save to database
                        details.TuitionFeesStatus = PaymentStatus.Paid;
                        var saveToDatabase = await _studentRepository.SavePaidStudentDetails(details);
                        return saveToDatabase;
                    }
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            //finally {_consumer.Close(); }    
        }

        public async Task<bool> ProduceDataAsync(StudentDetails studentDetails)
        {
            if(studentDetails != null && studentDetails.TuitionFees>=20000 && studentDetails.CGPA>=2)
            {
                try
                {
                    var produce = await _producer.ProduceAsync(_configuration["ClientConfigurations:Topic"],
                                        new Message<Null, string>
                                        {
                                            Value = JsonConvert.SerializeObject(studentDetails)
                                        });
                    _logger.LogInformation(produce.Value);
                   
                    return true;
                }
                catch (ProduceException<Null, string>ex)
                {
                    _logger.LogError(ex.Message);
                    throw;
                }
            }
            return false;
        }

       
    }
}
