namespace MovieSugges.Model
{
    public class MovieDetailResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string Poster { get; set; }

        public List<CommentResponse> Comments { get; set; }
    }
}
