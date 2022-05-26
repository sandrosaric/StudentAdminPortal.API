using System.Collections.Generic;

namespace StudentAdminPortal.API.DataModels.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsAsync();
    }
}
