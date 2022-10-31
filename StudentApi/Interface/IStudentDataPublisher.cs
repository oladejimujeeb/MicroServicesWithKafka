using StudentApi.Entities;

namespace StudentApi.Interface
{
    public interface IStudentDataPublisher
    {
        Task<bool> ProduceDataAsync(StudentDetails studentDetails);
        Task<bool>ConsumeDataAsync();
       
    }
}
