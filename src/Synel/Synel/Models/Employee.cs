using System.ComponentModel.DataAnnotations;

namespace Synel.Models
{
    public class Employees
    {
        [Key]
        public int Id { get; set; }
        public string PayrolNumber { get; set; }
        public string Fornames { get; set; }
        public string Surname { get; set; }
        public DateOnly DoB { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Postcode { get; set; }
        public string EmailHome { get; set; }
        public DateOnly StartDate { get; set; }
    }
}
