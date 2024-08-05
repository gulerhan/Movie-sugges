namespace MovieSugges.MovieSugges.BL.Model
{
    public class UserRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
