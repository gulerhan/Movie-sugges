using LoginBackend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginBackend.Controllers
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

        public IActionResult Create(MovieDTO movieDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movie = new Movie();

            movie.Title = movieDTO.Title;
            movie.Poster = movieDTO.Poster;
            movie.Point = movieDTO.Point;
            movie.CategoryId = movieDTO.CategoryId;

            dbContext.Movies.Add(movie);
            dbContext.SaveChanges();
            return Ok("Movie created successfully");

        }
        
        [HttpGet]
        [Route("GetAll")]

        public IActionResult GetMovies()
        {
            return Ok(dbContext.Movies.ToList());
        }

    }
}
