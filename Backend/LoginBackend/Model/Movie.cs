namespace LoginBackend.Model
{
    public class Movie
    {
        public int Id { get; set; }

        public string Poster { get; set; }
        public string Title { get; set; }

        public int Point { get; set; }

        public int CategoryId { get; set; }
    }
}
