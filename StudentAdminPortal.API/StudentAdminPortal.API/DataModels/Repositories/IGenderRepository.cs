namespace StudentAdminPortal.API.DataModels.Repositories
{
    public interface IGenderRepository
    {
        Task<List<Gender>> GetAllGendersAsync();
    }
}
