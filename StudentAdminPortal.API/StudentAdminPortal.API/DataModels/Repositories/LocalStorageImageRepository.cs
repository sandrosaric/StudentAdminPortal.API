namespace StudentAdminPortal.API.DataModels.Repositories
{
    public class LocalStorageImageRepository : IImageRepository
    {
        public async Task<string> Upload(IFormFile file, string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images",fileName);
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return GetServerPath(fileName);
        }


        private string GetServerPath(string filename)
        {
            return Path.Combine(@"Resources\Images", filename);
        }
    }
}
