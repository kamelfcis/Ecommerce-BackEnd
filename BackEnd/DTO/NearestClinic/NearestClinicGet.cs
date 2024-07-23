using BackEnd.Models;

namespace BackEnd.DTO.NearestClinic
{
    public class NearestClinicGet
    {
        public int Id { get; set; }
        public string ClinicName { get; set; }
        public string Location { get; set; } 
        public int RegionId { get; set; }
        public string RegionName { get; set; }
    }
}
