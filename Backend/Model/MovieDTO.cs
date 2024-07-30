namespace MovieSugges.Model
{
    public class MovieDTO
    {
        public IFormFile Poster { get; set; }
        public string Title { get; set; }

        public int Point { get; set; }

        public int CategoryId { get; set; }

        public string Description { get; set; }

    }
}
