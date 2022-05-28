using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.DataModels.Repositories;
using StudentAdminPortal.API.DomainModels;

namespace StudentAdminPortal.API.Controllers
{

    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentsController(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        [HttpGet("[controller]")]
        public async Task<ActionResult<List<StudentModel>>> Get()
        {
            List<StudentModel> result = null;
            try
            {
                List<Student> students = await _studentRepository.GetStudentsAsync();
                result = _mapper.Map<List<StudentModel>>(students);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }

        }


        [HttpGet("[controller]/{studentId:guid}")]
        public async Task<ActionResult<StudentModel>> Get([FromRoute] Guid studentId)
        {
            StudentModel result = null;

            try
            {
                var student = await _studentRepository.GetStudentByIdAsync(studentId);
                if (student == null)
                {
                    return this.StatusCode(404, "Student not found :(");
                }
                result = _mapper.Map<StudentModel>(student);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }


        }


        [HttpPut]
        [Route("[controller]/{studentId:guid}")]
        public async Task<ActionResult<StudentModel>> Update([FromRoute] Guid studentId, [FromBody] StudentFormModel studentFormModel)
        {
            StudentModel result = null;
            try
            {
                if (await _studentRepository.ExistsAsync(studentId))
                {

                    Student student = _mapper.Map<Student>(studentFormModel);

                    // Update Details
                    var updatedStudent = await _studentRepository.UpdateStudentAsync(studentId, student);
                    result = _mapper.Map<StudentModel>(updatedStudent);
                    return Ok(result);

                }
                return this.StatusCode(404, "Student not found");

            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }


        }



        [HttpDelete("[controller]/{studentId:guid}")]
        public async Task<ActionResult<StudentModel>> Delete([FromRoute] Guid studentId)
        {
            StudentModel result = null;
            try
            {
                Student deletedStudent =await  _studentRepository.DeleteStudentAsync(studentId);
                if (deletedStudent == null)
                {
                    return this.StatusCode(404, "Student not found.");
                }
                    
                result = _mapper.Map<StudentModel>(deletedStudent);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }
    }
}


