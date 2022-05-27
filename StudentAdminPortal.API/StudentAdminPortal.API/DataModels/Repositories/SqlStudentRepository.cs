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
       

    }
}
