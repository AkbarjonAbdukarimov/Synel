using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Synel.Models;

namespace Synel.Services
{
    public class EmployeeMapper : ClassMap<Employees>
    {
        //Mapping the csv file columns to the Employees model
        public EmployeeMapper()
        {

            Map(m => m.PayrolNumber).Name("Personnel_Records.Payroll_Number");
            Map(m => m.Fornames).Name("Personnel_Records.Forenames");
            Map(m => m.Surname).Name("Personnel_Records.Surname");
            Map(m => m.DoB).Name("Personnel_Records.Date_of_Birth").TypeConverter<DateOnlyConverter>();
            Map(m => m.Telephone).Name("Personnel_Records.Telephone");
            Map(m => m.Mobile).Name("Personnel_Records.Mobile");
            Map(m => m.Address).Name("Personnel_Records.Address");
            Map(m => m.Address2).Name("Personnel_Records.Address_2");
            Map(m => m.Postcode).Name("Personnel_Records.Postcode");
            Map(m => m.EmailHome).Name("Personnel_Records.EMail_Home");
            Map(m => m.StartDate).Name("Personnel_Records.Start_Date").TypeConverter<DateOnlyConverter>();



        }
    }
    //Converting string to DateOnly format
    public class DateOnlyConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            DateOnly date = DateOnly.Parse(text);

            return date;
        }
    }
}
