using Microsoft.EntityFrameworkCore;
using StudentApi.Entities;

namespace StudentApi.Context
{
    public class MyContext:DbContext
    {
        public MyContext(DbContextOptions<MyContext>options):base(options)    
        {

        }
        public DbSet<StudentDetails> StudentDetails { get; set; }
        public DbSet<Hostel>Hostels { get; set; }
    }
}
