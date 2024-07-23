using LoginBackend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

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
        public async Task<IActionResult> Create(MovieDTO movieDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movie = new Movie();

            movie.Title = movieDTO.Title;
            //movie.Poster = movieDTO.Poster;
            movie.Point = movieDTO.Point;
            movie.CategoryId = movieDTO.CategoryId;
            movie.Description = movieDTO.Description;

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
        public IActionResult GetMovie(int id)
        {
            return Ok(dbContext.Movies.Where(x=>x.Id==id).FirstOrDefault());
        }




        //if (p.Image != null)
        //    {
        // var resource = Directory.GetCurrentDirectory();
        //var extension = Path.GetExtension(p.Image.FileName);
        //var imagename = Guid.NewGuid() + extension;
        //var savelocation = resource + "/wwwroot/userimages/" + imagename;
        //var stream = new FileStream(savelocation, FileMode.Create);

        //await p.Image.CopyToAsync(stream);
        //user.ImageUrl = imagename;
        //    }
    }
}
