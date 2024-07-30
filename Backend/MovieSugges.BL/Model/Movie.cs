using System.ComponentModel.DataAnnotations.Schema;

namespace MovieSugges.MovieSugges.BL.Model
{
    public class Movie
    {
        public int Id { get; set; }

        public string Poster { get; set; }
        public string Title { get; set; }

        public int Point { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string Description { get; set; }

        public ICollection<Comment> Comments { get; set; }

    }
}
