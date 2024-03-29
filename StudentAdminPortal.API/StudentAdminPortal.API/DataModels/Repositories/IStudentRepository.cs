﻿using StudentAdminPortal.API.DomainModels;
using System.Collections.Generic;

namespace StudentAdminPortal.API.DataModels.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsAsync();
        Task<Student> GetStudentByIdAsync(Guid studentId);

        Task<bool> ExistsAsync(Guid studentId);
        Task<Student> UpdateStudentAsync(Guid studentId, Student student);
        Task<Student> DeleteStudentAsync(Guid studentId);
        Task<Student> PostStudentAsync(Student student);
        Task<bool> UpdateProfileImage(Guid studentId, string profileImageUrl);
    }
}
