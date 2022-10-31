

using Microsoft.EntityFrameworkCore;
using StudentApi.Context;
using StudentApi.Entities;
using StudentApi.Interface;

namespace StudentApi.Implementation
{
    public class StudentRepository : IStudentRepository
    {
        private readonly MyContext _context;
        public StudentRepository(MyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentPaymentConfirmation>> AllPaidStudent()
        {
            var students = await _context.StudentDetails.Include(x=>x.Hostel).ToListAsync();
            if (students is null) return null;
            return students.Select(x => new StudentPaymentConfirmation 
            { 
                CGPA = x.CGPA,
                Department = x.Department,
                HostelName = x.Hostel.HostelName,
                HostelReg = x.HostelReg,
                Level = x.Level,    
                MatricNo = x.MatricNo,
                PaymentDate = x.PaymentDate,
                PaymentRef = x.PaymentRef,
                StudentName = x.StudentName,
                TuitionFees = x.TuitionFees,
                TuitionFeesStatus = x.TuitionFeesStatus
            }).ToList();
            
        }

        public async Task<StudentPaymentConfirmation> ConfirmPaymentByMatricNo(string matricNo)
        {
            var student = await _context.StudentDetails.Include(x => x.Hostel).FirstOrDefaultAsync(x=> x.MatricNo==matricNo);
            if (student==null) return null;
            StudentPaymentConfirmation studentPayment = new() 
            {
                MatricNo=matricNo,
                Department=student.Department,
                CGPA=student.CGPA,
                HostelName=student.Hostel.HostelName,
                HostelReg=student.HostelReg,
                Level=student.Level,
                PaymentDate=student.PaymentDate,
                PaymentRef=student.PaymentRef,
                StudentName =student.StudentName,
                TuitionFees=student.TuitionFees,
                TuitionFeesStatus=student.TuitionFeesStatus
            };
            return studentPayment;
        }

        public async Task<bool> SavePaidStudentDetails(StudentDetails studentDetails)
        {
            try
            {
                var save = await _context.StudentDetails.AddAsync(studentDetails);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
