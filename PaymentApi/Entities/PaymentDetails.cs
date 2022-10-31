namespace PaymentApi.Entities
{
    public class PaymentDetails
    {
        public Guid Id { get; set; }
        public string StudentName { get; set; }
        public string? Department { get; set; }
        public int Level { get; set; }
        public double TuitionFees { get; set; }
        public string PaymentRef { get; set; }= Guid.NewGuid().ToString().Replace("-", "/").Substring(9);
        public PaymentStatus PaymentStatus { get; set; }
        public string MatricNo { get; set; }
        public int? HostelId { get; set; }
        public float CGPA { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public string HostelReg { get; set; } = Guid.NewGuid().ToString().Replace("-", "/").Substring(5);
    }
}
