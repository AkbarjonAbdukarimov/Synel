using CsvHelper;
using Synel.Types;
using System.Globalization;

namespace Synel.Services
{
    public  class FileService:IFileService
    {
        public FileService()
        {
            
        }
        public  List<T> ReadCSVFile<T>(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception("Please select a file to upload.");
            }
            if (!file.FileName.EndsWith(".csv"))
                throw new Exception("Please upload a valid CSV file.");
            List<T> list = new();

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<EmployeeMapper>();
                list = csv.GetRecords<T>().ToList();
            }
            return list;
        }
    }
}
