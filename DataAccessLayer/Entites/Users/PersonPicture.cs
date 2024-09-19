namespace DataAccessLayer.Entites.Users
{
    public class PersonPicture
    {
        public int PictureId { get; set; }
        public string PictureUrl { get; set; }
        public string Caption { get; set; }
        public int PersonId { get; set; }  // Foreign key

        // Navigation properties
        public Person Person { get; set; }
    }
}