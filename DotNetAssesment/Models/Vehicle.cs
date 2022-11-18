using System.Security.Claims;

namespace DotNetAssesment.Models
{
    public class Vehicle
    {
        public int ID { get; set; }
        public string Brand { get; set; }
        public string Vin { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        public int OwnerID { get; set; }
        //Allow this property to be nullable, so the form does not complain
        //OwnerID will be used instead to populate the field
        public Owner? Owner { get; set; }
        public List<Claim> Claims { get; set; }

        public Vehicle()
        {
            Brand = "";
            Vin = "";
            Color = "";
            Claims = new List<Claim>();
        }
    }
}
