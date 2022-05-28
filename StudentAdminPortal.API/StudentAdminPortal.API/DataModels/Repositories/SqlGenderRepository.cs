using Microsoft.EntityFrameworkCore;

namespace StudentAdminPortal.API.DataModels.Repositories
{
    public class SqlGenderRepository : IGenderRepository
    {

        private readonly StudentAdminContext _context;

        public SqlGenderRepository(StudentAdminContext context)
        {
            _context = context;
        }

        public async Task<List<Gender>> GetAllGendersAsync()
        {
            return await _context.Genders.ToListAsync();
        }
    }
}
