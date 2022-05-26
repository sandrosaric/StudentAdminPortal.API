using AutoMapper;
using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.DomainModels;

namespace StudentAdminPortal.API.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            this.CreateMap<Student,StudentModel>().ReverseMap();
            this.CreateMap<Gender,GenderModel>().ReverseMap();
            this.CreateMap<Address,AddressModel>().ReverseMap();
        }
    }
}
