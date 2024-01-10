using Domain.Data;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentsContext _studentsContext;
        public StudentsController(StudentsContext studentsContext)
        {
            _studentsContext = studentsContext;
        }

        [HttpGet]
        [Route("GetStudents")]
        public async Task<ActionResult<IEnumerable<StudentDetail>>> GetAllStudents()
        {
            try
            {
                if (_studentsContext.StudentDetails == null)
                {
                    return NotFound();
                }
                return await _studentsContext.StudentDetails.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetStudent/{StudId}")]
        public async Task<ActionResult<StudentDetail>> GetStudentsById(int StudId)
        {
            try
            {
                if (_studentsContext.StudentDetails == null)
                {
                    return NotFound();
                }
                var student = await _studentsContext.StudentDetails.FindAsync(StudId);
                if (student == null)
                {
                    return NotFound();
                }
                return student;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateStudent")]
        public async Task<IActionResult> CreateStudent([FromBody] StudentDetail student)
        {
            if (student != null)
            {
                StudentDetail createStudentModel = new StudentDetail();
                createStudentModel.StudentId = student.StudentId;
                createStudentModel.StudentRollNo = student.StudentRollNo;
                createStudentModel.StudentName = student.StudentName;
                createStudentModel.StudentPhone = student.StudentPhone;
                createStudentModel.StudentEmail = student.StudentEmail;
                createStudentModel.ClassId = student.ClassId;

                if (createStudentModel != null)
                {
                    _studentsContext.StudentDetails.Add(student);
                    await _studentsContext.SaveChangesAsync();
                }

                return CreatedAtAction(nameof(GetAllStudents), new { StudentId = createStudentModel?.StudentId }, createStudentModel);
            }

            return BadRequest("Invalid student data");
        }

        [HttpPut]
        [Route("UpdateStudent/{StudId}")]
        public async Task<IActionResult> UpdateStudent(int StudId, [FromBody] StudentDetail student)
        {
            if (StudId != student.StudentId)
            {
                return BadRequest("StudentId in the URL does not match the StudentId in the request body.");
            }

            try
            {
                var existingStudent = await _studentsContext.StudentDetails.FindAsync(StudId);

                if (existingStudent == null)
                {
                    return NotFound("Student not found.");
                }

                // Update properties of existingStudent with values from the request body
                existingStudent.StudentRollNo = student.StudentRollNo;
                existingStudent.StudentName = student.StudentName;
                existingStudent.StudentPhone = student.StudentPhone;
                existingStudent.StudentEmail = student.StudentEmail;
                existingStudent.ClassId = student.ClassId;

                // Update the database
                await _studentsContext.SaveChangesAsync();

                return Ok(existingStudent);  // You can customize the response as needed
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("DeleteStudent/{StudId}")]
        public async Task<ActionResult<StudentDetail>> DeleteStudentById(int StudId)
        {
            try
            {
                var student = await _studentsContext.StudentDetails.FindAsync(StudId);

                if (student == null)
                {
                    return NotFound($"Student with ID {StudId} not found.");
                }

                _studentsContext.StudentDetails.Remove(student);
                await _studentsContext.SaveChangesAsync();

                return Ok(student);  // You can customize the response as needed
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


    }
}
