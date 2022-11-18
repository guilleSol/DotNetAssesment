using System.ComponentModel.DataAnnotations;

namespace DotNetAssesment.Models
{
    public class Claim
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        //From here https://learn.microsoft.com/en-us/aspnet/core/mvc/views/working-with-forms?view=aspnetcore-7.0#the-input-tag-helper
        //So it does not display the time, just the date
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int VehicleID { get; set; }
        //Allow this property to be nullable, so the form does not complain
        //VehicleID will be used instead to populate the field
        public Vehicle? Vehicle { get; set; }

        public Claim()
        {
            Description = "";
            Status = "";
        }
    }
}
