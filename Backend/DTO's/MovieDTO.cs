﻿namespace MovieSugges.MovieSugges.BL.Model
{
    public class MovieDTO
    {
        public int UserId { get; set; }

        public IFormFile Poster { get; set; }
        public string Title { get; set; }

        public int Point { get; set; }

        public int CategoryId { get; set; }

        public string Description { get; set; }

    }
}
