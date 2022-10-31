using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentApi.Interface;

namespace PaymentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentDetailPublisher _dataPublisher;

        public PaymentController(IPaymentDetailPublisher dataPublisher)
        {
            _dataPublisher = dataPublisher;
        }

       
    }
}
