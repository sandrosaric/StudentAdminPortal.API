using AutoMapper;
using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Profiles.AfterMaps;

namespace StudentAdminPortal.API.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            this.CreateMap<Student,StudentModel>().ReverseMap();
            this.CreateMap<Gender,GenderModel>().ReverseMap();
            this.CreateMap<Address,AddressModel>().ReverseMap();
            CreateMap<Address, AddressModel>()
                 .ReverseMap();

            CreateMap<StudentFormModel, Student>()
                .AfterMap<StudentFormModelAfterMap>();


            CreateMap<StudentPostFormModel, Student>().
                AfterMap<StudentPostFormModelAfterMap>();

        }
    }
}
