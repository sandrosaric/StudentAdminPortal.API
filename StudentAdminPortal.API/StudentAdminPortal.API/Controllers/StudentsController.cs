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

        public StudentsController(IStudentRepository studentRepository,IMapper mapper)
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
    }
}
