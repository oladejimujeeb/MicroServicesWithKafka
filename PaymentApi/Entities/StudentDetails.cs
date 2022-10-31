
namespace PaymentApi.Entities
{
    public class StudentDetails
    {
        public Guid Id { get; set; }
        public string StudentName { get; set; }
        public string Department { get; set; }
        public int Level { get; set; }
        public float CGPA { get; set; }
        public double TuitionFees { get; set; }
        public string MatricNo { get; set; }
        public PaymentStatus TuitionFeesStatus { get; set; }
    }
}
