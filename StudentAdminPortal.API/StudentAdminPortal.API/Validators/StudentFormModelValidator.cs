using FluentValidation;
using StudentAdminPortal.API.DataModels.Repositories;
using StudentAdminPortal.API.DomainModels;

namespace StudentAdminPortal.API.Validators
{
    public class StudentFormModelValidator : AbstractValidator<StudentFormModel>
    {
        private readonly IGenderRepository _genderRepository;

        public StudentFormModelValidator(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Mobile).GreaterThan(99999).LessThan(100000000);
            RuleFor(x => x.GenderId).NotEmpty().Must(id =>
            {
                var gender = genderRepository.GetAllGendersAsync().Result.ToList().FirstOrDefault(x => x.Id == id);

                if (gender != null)
                {
                    return true;
                }
                return false;
            }).WithMessage("Please select a valid Gender");
            RuleFor(x => x.PhysicalAddress).NotEmpty();
            RuleFor(x => x.PostalAddress).NotEmpty();


        }
    }
}
