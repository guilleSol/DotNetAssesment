namespace DotNetAssesment.Models
{
    public class Claim
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
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
