using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace MovieSugges.MovieSugges.BL.Model
{
    public class User
    {
        public int UserId { get; set; } = 0;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Movie> Movies { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public int IsActive { get; set; } = 1;

        [ForeignKey("UserRole")]
        public int RoleId { get; set; }
        public UserRole Role { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;

    }


}
