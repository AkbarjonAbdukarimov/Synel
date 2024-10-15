namespace Synel.Types
{
    public interface IFileService
    {
        public List<T> ReadCSVFile<T>(IFormFile file);
    }
}
