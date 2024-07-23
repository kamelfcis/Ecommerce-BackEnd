using BackEnd.Models;

namespace BackEnd.DTO.NearestClinic
{
    public class NearestClinicAdd
    { 
        public string ClinicName { get; set; }
        public string Location { get; set; } 
        public int RegionId { get; set; }
    }
}
