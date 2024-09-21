using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class PersonDTO
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string IdentityCard { get; set; }
        public string PassportID { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string PersonType { get; set; }
        public List<PersonPictureDTO> PersonPictures { get; set; } = new List<PersonPictureDTO>();
    }

    public class PersonPictureDTO
    {
        public int PictureId { get; set; }
        public string PictureUrl { get; set; }
        public string Caption { get; set; }
    }
}
