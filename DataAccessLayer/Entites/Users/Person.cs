using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entites.Users
{
    public class Person
    {
        public int PersonId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(6)]
        public string Gender { get; set; }

        [Required]
        [StringLength(17)]
        public string? IdentityCard { get; set; }

        [StringLength(9)]
        public string? PassportID { get; set; }

        [Required]
        [StringLength(50)]
        public string Mobile { get; set; }


        [StringLength(500)]
        public string? Address { get; set; }

        public string PersonType { get; set; }

        public int CityId { get; set; }

        public string ApplicationUserId { get; set; }

        public City City { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime DateofBirth { get; set; }

        // Navigation properties from Client
        //public ICollection<ClientDestination> ClientDestinations { get; set; }
        //public ICollection<Booking> Bookings { get; set; }
        //public ICollection<CustomerSupport> CustomerSupports { get; set; }
        //public ICollection<VisaAssistanceService> VisaAssistanceServices { get; set; }

        // Existing UserInfo navigation properties
        public ICollection<PersonPicture> Pictures { get; set; }
        //public ICollection<BlogComment> BlogComments { get; set; }
        //public ICollection<BlogPost> BlogPosts { get; set; }
        //public virtual ICollection<Review> Reviews { get; set; }
        //public virtual ICollection<Wishlist> Wishlists { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
