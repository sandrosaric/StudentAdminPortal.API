using System.Collections.Generic;

namespace StudentAdminPortal.API.DataModels.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsAsync();
        Task<Student> GetStudentByIdAsync(Guid studentId);

        Task<bool> ExistsAsync(Guid studentId);
        Task<Student> UpdateStudentAsync(Guid studentId, Student student);
    }
}
