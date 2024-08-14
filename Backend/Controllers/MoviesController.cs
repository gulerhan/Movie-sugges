using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using MovieSugges.MovieSugges.BL.Model;

namespace MovieSugges.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MyDbContext dbContext;

        public MoviesController(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(MovieDTO movieDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movie = new Movie();

            movie.Title = movieDTO.Title;
            movie.Point = movieDTO.Point;
            movie.CategoryId = movieDTO.CategoryId;
            movie.Description = movieDTO.Description;
            //movie.UserId = movieDTO.UserId;

            var resource = Directory.GetCurrentDirectory();
            var extension = Path.GetExtension(movieDTO.Poster.FileName);
            var postername = Guid.NewGuid() + extension;
            var savelocation = resource + "/Upload/MovieImages/" + postername;
            var stream = new FileStream(savelocation, FileMode.Create);

            await movieDTO.Poster.CopyToAsync(stream);
            movie.Poster = postername;

            dbContext.Movies.Add(movie);
            await dbContext.SaveChangesAsync();
            return Ok("Movie created successfully");

        }



        [HttpGet]
        [Route("GetAll")]

        public IActionResult GetMovies()
        {
            return Ok(dbContext.Movies.ToList());
        }

        [HttpGet]
        [Route("GetDetail/{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Linq nedir? Linq'nun kullanımı ve fonksiyonları. Aşağıda kullandığın fonksiyonlar nedir ne işe yarar?
            var movie = await dbContext.Movies
                    .Include(m => m.Comments)
                        .ThenInclude(c => c.User)
                    .Include(m => m.Category)
                    .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            var response = new MovieDetailResponse
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Poster = movie.Poster,
                CategoryName = movie.Category.Name,
                Comments = movie.Comments.Select(c => new CommentResponse
                {
                    Id = c.Id,
                    Content = c.Content,
                    UserName = c.User.FirstName + " " + c.User.LastName,
                }).ToList()
            };

            return Ok(response);
        }

        [HttpPost]
        [Route("Comment")]

        public async Task<IActionResult> AddComment(CommentDTO commentDto)
        {
            var movie = await dbContext.Movies.FindAsync(commentDto.MovieId);
            if (movie == null)
            {
                return NotFound();
            }

            var comment = new Comment
            {
                Content = commentDto.Content,
                MovieId = commentDto.MovieId,
                UserId = commentDto.UserId,
                CreatedAt = DateTime.UtcNow,
            };

            dbContext.Comments.Add(comment);
            await dbContext.SaveChangesAsync();
            return Ok("Comment created successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await dbContext.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            dbContext.Movies.Remove(movie);
            await dbContext.SaveChangesAsync();

            return Ok("Movie deleted successfully");
        }

        [HttpGet]
        [Route("GetMyMovies/{userId}")]
        public async Task<IActionResult> GetMyMovies(int userId)
        {
            if (userId == null)
            {
                return NotFound();
            }
            var movies = await dbContext.Movies
                .Include(m => m.Comments) 
                .Include(m => m.Category) 
                //.Where(m => m.UserId == userId)
                .ToListAsync();

            if (movies == null)
            {
                return NotFound();

            }

            var response = new List<MovieDetailResponse>();
            foreach (var movie in movies)
            {
                var detail  = new MovieDetailResponse
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Description = movie.Description,
                    Poster = movie.Poster,
                    CategoryName = movie.Category.Name,
                    Comments = movie.Comments.Select(c => new CommentResponse
                    {
                        Id = c.Id,
                        Content = c.Content,
                        UserName = c.User.FirstName + " " + c.User.LastName,
                    }).ToList()
                };

                response.Add(detail);
            }

            return Ok(response);
        }


    }
}
