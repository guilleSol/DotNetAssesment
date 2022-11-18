namespace DotNetAssesment.Models
{
    public class Owner
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DriverLicense { get; set; }
        public List<Vehicle> Vehicles { get; set; }

        public Owner()
        {
            FirstName = "";
            LastName = "";
            DriverLicense = "";
            Vehicles = new List<Vehicle>();
        }
    }
}
