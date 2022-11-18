namespace DotNetAssesment.Models
{
    public class Claim
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public int VehicleID { get; set; }
        public Vehicle? Vehicle { get; set; }

    }
}
