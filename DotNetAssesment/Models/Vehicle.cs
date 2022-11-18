using System.Security.Claims;

namespace DotNetAssesment.Models
{
    public class Vehicle
    {
        public int ID { get; set; }
        public string Brand { get; set; }
        public string Vin { get; set; }
        public String Color { get; set; }
        public int Year { get; set; }
        public int OwnerID { get; set; }
        public Owner? Owner { get; set; }
        public List<Claim> Claims { get; set; }

        public Vehicle()
        {
            Claims = new List<Claim>();
        }
    }
}
