
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
        [Route("GetUserComments/{userId}")]
        public async Task<IActionResult> GetUserComments(int userId)
        {
            if (userId == null)
            {
                return NotFound();
            }
            var comments = await dbContext.Comments
                .Include(x => x.User)
                .Include(c => c.Movie)
                  .ThenInclude(m => m.Category)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (comments == null)
            {
                return NotFound();

            }

            //var response = new List<UserCommentsResponse>();
            //foreach (var comment in comments)
            //{
            //    var userComment = new UserCommentsResponse
            //    {
            //       Movie = comment.Movie,
            //       Comment = comment,
            //       User = comment.User
            //    };

            //    response.Add(userComment);
            //}

            return Ok(comments);
        }


        [HttpGet]
        [Route("GetMovieComments/{movieid}")]
        public IActionResult GetMovieComments(int movieid)
        {
            return Ok(dbContext.Comments.Where(comm => comm.MovieId == movieid).ToList());
        }
    }
}
