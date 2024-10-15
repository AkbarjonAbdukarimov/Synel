using Microsoft.AspNetCore.Http;
using NuGet.ContentModel;
using Synel.Models;
using Synel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynelTests
{
    public class FileServiceTests
    {
        [Fact]
        public void Reads_CSV_And_Returns_List()
        {

            // Read CSV file and pass it as FormFile
            string filePath = "D:\\Projects\\Synel\\tests\\SynelTests\\dataset.csv";
            byte[] fileBytes = File.ReadAllBytes(filePath);
            IFormFile formFile = new FormFile(new MemoryStream(fileBytes), 0, fileBytes.Length, "file", "file.csv");
            //Act
            FileService fileService = new FileService();
            List<Employees> list = fileService.ReadCSVFile<Employees>(formFile);
            //Asset
            Assert.NotNull(list);
            Assert.IsType<List<Employees>>(list);
            Assert.NotEmpty(list);





        }

    }
}
