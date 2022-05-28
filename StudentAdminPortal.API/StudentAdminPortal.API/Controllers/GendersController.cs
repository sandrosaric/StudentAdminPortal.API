using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DataModels.Repositories;
using StudentAdminPortal.API.DomainModels;

namespace StudentAdminPortal.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GendersController : ControllerBase
    {
        private readonly IGenderRepository _genderRepository;
        private readonly IMapper _mapper;

        public GendersController(IGenderRepository genderRepository, IMapper mapper)
        {
            _genderRepository = genderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GenderModel>>> Get()
        {
            List<GenderModel> result = null;

            try
            {
                var genderList = await _genderRepository.GetAllGendersAsync();
                if (genderList == null)
                    return this.StatusCode(404, "Gender list not found :(");
                result = _mapper.Map<List<GenderModel>>(genderList);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }


        
    }
}
