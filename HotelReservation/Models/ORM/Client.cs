using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservation.Models.ORM
{
    public class Client : BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string EMail { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
    
}
