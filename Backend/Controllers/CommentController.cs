
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieSugges.Migrations;
using MovieSugges.MovieSugges.BL.Model;

namespace MovieSugges.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : Controller
    {

        private readonly MyDbContext dbContext;

        public CommentController(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CommentDTO commentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = new Comment();
            comment.Content = commentDTO.Content;
            comment.UserId = commentDTO.UserId;
            comment.CreatedAt = DateTime.Now;

            dbContext.Comments.Add(comment);
            await dbContext.SaveChangesAsync();
            return Ok("Comment created successfully");

        }

        [HttpGet]
        [Route("GetMovieComments/{movieid}")]
        public IActionResult GetMovieComments(int movieid)
        {
            return Ok(dbContext.Comments.Where(comm => comm.MovieId == movieid).ToList());
        }
    }
}
