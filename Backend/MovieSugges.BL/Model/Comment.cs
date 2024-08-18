using System.ComponentModel.DataAnnotations.Schema;

namespace MovieSugges.MovieSugges.BL.Model
{
    public class Comment
    {
        public int Id { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
