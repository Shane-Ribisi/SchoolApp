using Microsoft.AspNetCore.Mvc;
using SchoolApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SchoolApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        [HttpGet]
        [Route("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents() 
        {
            var students = new List<StudentDTO>();
            foreach (var item in SchoolRepository.Students)
            {
                StudentDTO obj = new StudentDTO()
                {
                    Id = item.Id,
                    StudentName = item.StudentName,
                    Email = item.Email,
                    Address = item.Address
                };
                students.Add(obj);
            }

            //We can also use linq as an alternative
            //var students = SchoolRepository.Students.Select(s => new StudentDTO()
            //{
            //    Id = s.Id,
            //    StudentName = s.StudentName,
            //    Email = s.Email,
            //    Address = s.Address
            //};
            return Ok(students);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Student> GetStudentById(int id) 
        {
            //400 - Bad request client error
            if (id <= 0)
                return BadRequest();

            //404 - Not found client error
            var student = SchoolRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            if(student == null)
                return NotFound($"Student with Id {id} not found!!");

            var studentDTO = new StudentDTO()
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address
            };
            //200 - Success
            return Ok(studentDTO);

            //return SchoolRepository.Students.Where(n => n.Id == id).FirstOrDefault();
        }

        [HttpGet("{name:alpha}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentByName(string name)
        {
            // 400 - Bad request client error
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            //404 - Not found client error
            var student = SchoolRepository.Students.Where(n => n.StudentName == name).FirstOrDefault();
            if (student == null)
                return NotFound($"Student with the Name -> {name} was not found!!");

            var studentDTO = new StudentDTO()
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address
            };

            //200 - Success
            return Ok(studentDTO);
            //return SchoolRepository.Students.Where(n => n.StudentName == name).FirstOrDefault();
        }

        [HttpPost("CreateNew")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> CreateNewStudent([FromBody]StudentDTO model)
        {
            if(model == null)
                return BadRequest();
            int newId = SchoolRepository.Students.LastOrDefault().Id + 1;
            Student student = new Student()
            {
                Id = newId,
                StudentName = model.StudentName,
                Email = model.Email,
                Address = model.Address
            };

            SchoolRepository.Students.Add(student);

            model.Id = student.Id;

            return CreatedAtRoute("GetStudentById", new { id = model.Id }, model);
            //return Ok(model);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> DeleteStudent(int id)
        {
            // 400 - Bad request client error
            if (id <= 0)
                return BadRequest();

            //404 - Not found client error
            var student = SchoolRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
                return NotFound($"Student with Id={id} not found!!");

            //200 - Success
            //return Ok(student);
            //var student = SchoolRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            SchoolRepository.Students.Remove(student);

            return Ok(true);
        }
    }
}
