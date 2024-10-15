using Microsoft.EntityFrameworkCore;
using Moq;
using Synel.Data;
using Synel.Models;
using Synel.Services;
using Synel.Types;

namespace SynelTests
{
    public class EmployeeServiceTest
    {
        List<Employees> list;

        public EmployeeServiceTest()
        {
            list = new List<Employees>
        {
            new Employees
            {
                Id = 1,
                Fornames = "John",
                Surname = "Doe",
                PayrolNumber = "COOP08",
                DoB = new DateOnly(1955, 1, 26),
                Telephone = "12345678",
                Mobile = "987654231",
                Address = "12 Foreman Road",
                Address2 = "London",
                Postcode = "GU12 6JW",
                EmailHome = "nomadic20@hotmail.co.uk",
                StartDate = new DateOnly(2013, 4, 18)
            },
            new Employees
            {
                Id = 2,
                Fornames = "Jerry",
                Surname = "Jackson",
                PayrolNumber = "JACK13",
                DoB = new DateOnly(1974, 5, 11),
                Telephone = "2050508",
                Mobile = "6987457",
                Address = "115 Spinney Road",
                Address2 = "Luton",
                Postcode = "LU33DF",
                EmailHome = "gerry.jackson@bt.com",
                StartDate = new DateOnly(2013, 4, 18)
            }
        };

        }

        [Fact]
        public async Task Checks_Insertion_To_DB()
        {
            //Arrange

            //Mocking db
            var mockOpts = new DbContextOptionsBuilder<SynelContext>()
            .UseInMemoryDatabase(databaseName: "Synel")
            .Options;




            using (var mockContext = new SynelContext(mockOpts))
            {
                //cleaning db to avoid duplicates
                mockContext.Employees.RemoveRange(mockContext.Employees);

                var mockFileService = new Mock<IFileService>();
                mockFileService.Setup(s => s.ReadCSVFile<Employees>(null)).Returns(list);
                var service = new EmployeeService(mockContext, mockFileService.Object);

                //Act
                var rows = await service.InsertFileDataAsync(null);
                // Assert: Verify that AddRange was called with the correct list
                Assert.Equal(2, rows);


            }


        }
        [Fact]
        public async Task Checks_Returning_Employees()
        {
            var employeesData = list.AsQueryable();
            //Mocking db
            var mockOpts = new DbContextOptionsBuilder<SynelContext>()
            .UseInMemoryDatabase(databaseName: "Synel")
            .Options;




            using (var mockContext = new SynelContext(mockOpts))
            {
                mockContext.Employees.AddRange(list);
                var rows = await mockContext.SaveChangesAsync();
                var service = new EmployeeService(mockContext);

                //Act
                var returnedList = await service.GetEmployeesAsync();
                // Assert: Verify that AddRange was called with the correct list
                Assert.Equal(2, returnedList.Count);

                Assert.Equal<List<Employees>>(returnedList, list);


            }
        }

        [Fact]
        public async Task Checks_Search()
        {

            //Mocking db
            var mockOpts = new DbContextOptionsBuilder<SynelContext>()
            .UseInMemoryDatabase(databaseName: "Synel")
            .Options;

            using (var mockContext = new SynelContext(mockOpts))
            {
                if (mockContext.Employees.Count() == 0)
                {
                    mockContext.Employees.AddRange(list);
                    var rows = await mockContext.SaveChangesAsync();
                }

                var service = new EmployeeService(mockContext);

                //Act
                var returnedList = await service.GetEmployeesAsync("John");
                // Assert: Verify that returns one user by name 
                Assert.IsType<List<Employees>>(returnedList);
                Assert.Single(returnedList);
                Assert.Equal("John", returnedList[0].Fornames);



            }
        }
        [Fact]
        public async Task Checks_Filter()
        {
            var employeesData = list.AsQueryable();
            //Mocking db
            var mockOpts = new DbContextOptionsBuilder<SynelContext>()
            .UseInMemoryDatabase(databaseName: "Synel")
            .Options;




            using (var mockContext = new SynelContext(mockOpts))
            {
                if (mockContext.Employees.Count() == 0)
                {
                    mockContext.Employees.AddRange(list);
                    var rows = await mockContext.SaveChangesAsync();
                }
                var service = new EmployeeService(mockContext);


                var sorted = new List<Employees>(list);

                //Act
                sorted.Sort((x, y) => y.Surname.CompareTo(x.Surname));
                var returnedList = await service.GetEmployeesAsync(null, SortOrder.DESC, SortFields.Surname);
                // Assert: Verify that returns one user by name 
                Assert.IsType<List<Employees>>(returnedList);
                Assert.Equal(returnedList[0].Id, sorted[0].Id);
                Assert.StartsWith("J", returnedList[0].Surname);
                Assert.StartsWith("D", returnedList[1].Surname);



            }
        }

        [Fact]
        public async Task Checks_Updating()
        {
            //Arrange
            var mockOpts = new DbContextOptionsBuilder<SynelContext>()
           .UseInMemoryDatabase(databaseName: "Synel")
           .Options;




            using (SynelContext mockContext = new(mockOpts))
            {
                if (mockContext.Employees.Count() == 0)
                {
                    mockContext.Employees.AddRange(list);
                    var rows = await mockContext.SaveChangesAsync();
                }
                var service = new EmployeeService(mockContext);

                list[0].Fornames = "Jane";
                list[0].Surname = "Doe";
                //Act
                var isUpdated = await service.UpdateEmployeeAsync(list[0]);
                // Assert
                Assert.True(isUpdated);




            }
        }

        [Fact]
        public async Task Check_To_Get_Employee_By_Id()
        {
            //Arrange
            var mockOpts = new DbContextOptionsBuilder<SynelContext>()
           .UseInMemoryDatabase(databaseName: "Synel")
           .Options;
            using (var mockContext = new SynelContext(mockOpts))
            {
                if (mockContext.Employees.Count() == 0)
                {
                    mockContext.Employees.AddRange(list);
                    var rows = await mockContext.SaveChangesAsync();
                }
                var service = new EmployeeService(mockContext);

                //Act
                var item = await service.GetEmployeeByIdAsync(1);
                // Assert: Verify that AddRange was called with the correct list
                Assert.Equal(list[0].Id, item.Id);
                Assert.Equal(list[0].Surname, item.Surname);
                Assert.Equal(list[0].PayrolNumber, item.PayrolNumber);


            }
        }
    }
}
