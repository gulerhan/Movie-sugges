namespace MovieSugges.MovieSugges.BL.Model
{
    public class UserCommentsResponse
    {
        public Comment Comment  { get; set; }
        public Movie Movie { get; set; }

        public User User { get; set; }

    }
}
