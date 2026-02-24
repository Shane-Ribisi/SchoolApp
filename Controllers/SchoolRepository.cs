using SchoolApp.Models;

namespace SchoolApp.Controllers
{
    public static class SchoolRepository
    {
        public static List<Student> Students { get; set; } = new List<Student>()
        {
            new Student
            {
                Id = 1,
                StudentName = "Shane",
                Email = "shane@gmail.com",
                Address = "69 Sauer Newtown"
            },
            new Student
            {
                Id = 2,
                StudentName = "Ribisi",
                Email = "ribisi@gmail.com",
                Address = "69 Ingenieur Nellmapius"
            }
        };
    }
}
