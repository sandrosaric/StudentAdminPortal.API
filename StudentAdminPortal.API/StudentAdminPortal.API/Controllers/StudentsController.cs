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
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public StudentsController(IStudentRepository studentRepository,IImageRepository imageRepository, IMapper mapper,LinkGenerator linkGenerator)
        {
            _studentRepository = studentRepository;
           _imageRepository = imageRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet("[controller]"),ActionName("GetAllStudents")]
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



        [HttpPost("[controller]")]
        public async Task<ActionResult<StudentModel>> Post([FromBody] StudentPostFormModel studentPostFormModel)
        {
            StudentModel result = null;
            try
            {
                Student student = _mapper.Map<Student>(studentPostFormModel);
                Student createdStudent = await _studentRepository.PostStudentAsync(student);
                if (createdStudent == null)
                    return this.StatusCode(400, "Bad request");
                result = _mapper.Map<StudentModel>(createdStudent);
                
                return CreatedAtAction("GetAllStudents", new {studentId=result.Id}, createdStudent);
                
            }
            catch(Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }

        [HttpPost("[controller]/{studentId:guid}/upload-image")]
        public async Task<ActionResult> UploadImage([FromRoute] Guid studentId,IFormFile profileImage)
        {
            var supportedExtensions = new List<string>()
            {
                "jpg",
                "jpeg",
                "png",
                "gif"
            };
            if(profileImage != null && profileImage.Length > 0)
            {
                var extension = Path.GetExtension(profileImage.FileName);
                if (supportedExtensions.Contains(extension))
                {
                    if (await _studentRepository.ExistsAsync(studentId))
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);
                        var fileImagePath = await _imageRepository.Upload(profileImage, fileName);
                        bool success = await _studentRepository.UpdateProfileImage(studentId, fileImagePath);
                        if (success)
                        {
                            return Ok(fileImagePath);
                        }
                        else
                        {
                            return this.StatusCode(StatusCodes.Status500InternalServerError, "Error uploading image :(");
                        }
                    }
                }
            }
           
            
            return this.StatusCode(400, "This is not a valid image format.");
        }
    }
}


