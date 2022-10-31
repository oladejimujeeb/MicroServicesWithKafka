namespace StudentApi.Entities
{
    public class StudentDetailRequest
    {
        public string StudentName { get; set; }
        public string Department { get; set; }
        public int Level { get; set; }
        public float CGPA { get; set; }
        public double TuitionFees { get; set; }
        public string MatricNo { get; set; }
    }
}
