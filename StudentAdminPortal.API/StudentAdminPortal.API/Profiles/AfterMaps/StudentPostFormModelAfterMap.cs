using AutoMapper;
using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.DomainModels;

namespace StudentAdminPortal.API.Profiles.AfterMaps
{
    public class StudentPostFormModelAfterMap : IMappingAction<StudentPostFormModel, Student>
    {
        public void Process(StudentPostFormModel source, Student destination, ResolutionContext context)
        {
            destination.Id = Guid.NewGuid();
            destination.Address = new Address()
            {
                Id = Guid.NewGuid(),
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress,
                StudentId = Guid.NewGuid(),
                
            };
        }
    }
}
