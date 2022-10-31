namespace StudentApi.Entities
{
    public class StudentDetails
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string StudentName { get; set; } 
        public string Department { get; set; }
        public int Level { get; set; }  
        public float CGPA { get; set; } 
        public double TuitionFees { get; set; } 
        public string MatricNo { get; set; }
        public int HostelId { get; set; }
        public Hostel Hostel { get; set; }
        public DateTime PaymentDate { get; set; } 
        public PaymentStatus? TuitionFeesStatus { get; set; }
        public string HostelReg { get; set; }
        public string PaymentRef { get; set; }

    }
}
