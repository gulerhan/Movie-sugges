namespace MovieSugges.MovieSugges.BL.Model
{
    public class UserAndRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public virtual User User { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}
