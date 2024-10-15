using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Synel.Controllers;
using Synel.Models;
using Synel.Types;

namespace SynelTests
{
    public class EmployeesControllerTest
    {
        private IEmployeeService mockService = new MockEmployeeService();
        [Fact]
        public async Task RetunsIndexViewWithEmployees()
        {

            // Arrange


            EmployeesController controller = new(mockService);

            // Act
            var result = await controller.Index();

            // Assert
            Assert.NotNull(result);
            // checking controller returns view
            var resultView = Assert.IsType<ViewResult>(result);
            Assert.NotNull(resultView);
            // checking that data model is same as expected
            var model = Assert.IsAssignableFrom<IEnumerable<Employees>>(resultView.ViewData.Model);
        }



        [Fact]
        public void ChecksIfItReturnCreateView()
        {
            //Arrange
            EmployeesController controller = new(mockService);
            //Act
            var result = controller.Create();
            //Assert
            Assert.NotNull(result);



        }

        [Fact]
        public async Task Checks_If_It_Redirects_After_Insert_With_Affected_Row_Count()
        {
            //Arrange
            EmployeesController controller = new(mockService);
            string data = @"Personnel_Records.Payroll_Number,Personnel_Records.Forenames,Personnel_Records.Surname,Personnel_Records.Date_of_Birth,Personnel_Records.Telephone,Personnel_Records.Mobile,Personnel_Records.Address,Personnel_Records.Address_2,Personnel_Records.Postcode,Personnel_Records.EMail_Home,Personnel_Records.Start_Date
            COOP08,John ,William,26/01/1955,12345678,987654231,12 Foreman road,London,GU12 6JW,nomadic20@hotmail.co.uk,18/04/2013
            JACK13,Jerry,Jackson,11/5/1974,2050508,6987457,115 Spinney Road,Luton,LU33DF,gerry.jackson@bt.com,18/04/2013
            ";
            var fileContent = new MemoryStream();
            var writeStream = new StreamWriter(fileContent);
            //Act
            writeStream.Write(data);
            writeStream.Flush();
            fileContent.Position = 0;
            IFormFile formFile = new FormFile(fileContent, 0, fileContent.Length, "file", "employees.csv");
            RedirectToActionResult result = (RedirectToActionResult)await controller.Create(formFile);
            //Assert
            var resultView = Assert.IsType<RedirectToActionResult>(result);
            //checking if it redirects to Index
            Assert.Equal("Index", resultView.ActionName);
            //checking if it passes the rows affected
            Assert.Equal(2, resultView.RouteValues["rowsAffected"]);



        }
        [Fact]
        public async Task Check_To_Get_Edit_Form()
        {
            //Arrange
            EmployeesController controller = new(mockService);
            //Act
            IActionResult result = await controller.Edit(1);

            //Assert
            Assert.NotNull(result);
            var resultView = Assert.IsType<ViewResult>(result);

            //checking model
            var model = Assert.IsAssignableFrom<Employees>(resultView.ViewData.Model);
            //checking if it returs correct value
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public async Task Updates_User()
        {
            //Arrange
            var controller=new EmployeesController(mockService);

            //Act
            var result = await controller.Edit(1, new Employees() { Id=1});

            //Assert
            var resultView = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            //checking if it redirects to Index
            Assert.Equal("Index", resultView.ActionName);

        }


    }
}
