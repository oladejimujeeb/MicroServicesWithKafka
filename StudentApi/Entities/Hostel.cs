namespace StudentApi.Entities
{
    public class Hostel
    {
        public int Id { get; set; }
        public string HostelName { get; set; }  
        public string HostelReg { get; set; }
        public List<StudentDetails> StudentDetails { get; set; } = new List<StudentDetails>();
    }
}
