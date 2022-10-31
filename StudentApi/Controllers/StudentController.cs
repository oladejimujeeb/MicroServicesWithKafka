using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApi.Entities;
using StudentApi.Interface;
using System.Net;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentDataPublisher _dataPublisher;
        private readonly IStudentRepository _studentRepository; 
        public StudentController(IStudentDataPublisher dataPublisher, IStudentRepository studentRepository)
        {
            _dataPublisher = dataPublisher;
            _studentRepository = studentRepository;
        }
        [HttpPost]
        public async Task<IActionResult>StudentDetails(StudentDetailRequest request)
        {
            Random random = new Random();
            var stuDetail = new StudentDetails()
            {
                CGPA = request.CGPA,
                Department = request.Department,
                Level = request.Level,
                StudentName = request.StudentName,
                TuitionFees = request.TuitionFees,
                TuitionFeesStatus = PaymentStatus.Pending,
                MatricNo = request.MatricNo
            };
            var post = await _dataPublisher.ProduceDataAsync(stuDetail);
            if (post)
            {
                return Ok();
               
            }
            return BadRequest("Amount or CGPA is less than required, See you HOD");
        }
        [HttpGet("ConfirmPayment/{matricNo}")]
        public async Task<IActionResult> PaymentConfirmation(string matricNo)
        {
            if (matricNo is null)
            {
                return NotFound("Check your matric number");
            }

            var confirm = await _studentRepository.ConfirmPaymentByMatricNo(matricNo);
            if(confirm is null)
            {
                return NotFound("Cannot confirm payment, check your matric number");
            }
            return Ok(confirm);
        }
        [HttpGet("AllStudent")]
        public async Task<IActionResult> AllConfirmPayment()
        {
            var payments = await _studentRepository.AllPaidStudent();
            if(payments is null)
            {
                return NoContent();
            }
            return Ok(payments);
        }
        [HttpPost("ConsumerDataandSave")]
        public async Task<IActionResult>RuningKafKa()
        {
            //this method is suppose to be in the background service
            var run = await _dataPublisher.ConsumeDataAsync();
            if (!run) return BadRequest("something went wrong");

            return Ok("Save");
        }
    }
}
