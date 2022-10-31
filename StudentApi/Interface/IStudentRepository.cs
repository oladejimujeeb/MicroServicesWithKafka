using StudentApi.Entities;
using System.Collections.Generic;

namespace StudentApi.Interface
{
    public interface IStudentRepository
    {
        Task<bool>SavePaidStudentDetails(StudentDetails studentDetails);
        Task<IEnumerable<StudentPaymentConfirmation>>AllPaidStudent();

        Task<StudentPaymentConfirmation> ConfirmPaymentByMatricNo(string matricNo);
    }
}
