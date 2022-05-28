using Microsoft.EntityFrameworkCore;

namespace StudentAdminPortal.API.DataModels.Repositories
{
    public class SqlStudentRepository : IStudentRepository
    {
        private readonly StudentAdminContext _context;

        public SqlStudentRepository(StudentAdminContext context)
        {
            _context = context;
        }

       
        public async Task<Student> GetStudentByIdAsync(Guid studentId)
        {
            return await _context.Students.Include(nameof(Gender)).Include(nameof(Address)).FirstOrDefaultAsync(s => s.Id == studentId);
            
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _context.Students.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }

        public async Task<bool> ExistsAsync(Guid studentId)
        {
            return await _context.Students.AnyAsync(x => x.Id == studentId);
        }

        public async Task<Student> UpdateStudentAsync(Guid studentId, Student student)
        {
            var existingStudent = await GetStudentByIdAsync(studentId);
            if (existingStudent != null)
            {
                existingStudent.FirstName = student.FirstName;
                existingStudent.LastName = student.LastName;
                existingStudent.DateOfBirth = student.DateOfBirth;
                existingStudent.Email = student.Email;
                existingStudent.Mobile = student.Mobile;
                existingStudent.GenderId = student.GenderId;
                existingStudent.Address.PhysicalAddress = student.Address.PhysicalAddress;
                existingStudent.Address.PostalAddress = student.Address.PostalAddress;
                

                await _context.SaveChangesAsync();
                return existingStudent;
            }

            return null;
        }

        public async Task<Student> DeleteStudentAsync(Guid studentId)
        {
            var student = await GetStudentByIdAsync(studentId);
            if(student != null)
            {
                 _context.Students.Remove(student);
                await  _context.SaveChangesAsync();
                return student;
            }
            return null;
        }
    }
}
