using Moq;
using Synel.Data;
using Synel.Models;
using System.Data.Entity;

namespace SynelTests
{
    public class MockDbContex
    {
        List<Employees> list = new();
        public MockDbContex()
        {
            var e1 = new Employees
            {
                Id = 2,
                PayrolNumber = "JACK13",
                Fornames = "Jerry",
                Surname = "Jackson",
                DoB = DateOnly.Parse("11/5/1974"),
                Telephone = "2050508",
                Mobile = "6987457",
                Address = "115 Spinney Road",
                Address2 = "Luton",
                Postcode = "LU33DF",
                EmailHome = "gerry.jackson@bt.com",
                StartDate = DateOnly.Parse("18/04/2013")
            };
            list.Add(e1);
            var e2 = new Employees
            {
                Id = 2,
                PayrolNumber = "JACK13",
                Fornames = "Jerry",
                Surname = "Jackson",
                DoB = DateOnly.Parse("11/5/1974"),
                Telephone = "2050508",
                Mobile = "6987457",
                Address = "115 Spinney Road",
                Address2 = "Luton",
                Postcode = "LU33DF",
                EmailHome = "gerry.jackson@bt.com",
                StartDate = DateOnly.Parse("18/04/2013")
            }
            ;
            list.Add(e2);
        }
       










    }
}
