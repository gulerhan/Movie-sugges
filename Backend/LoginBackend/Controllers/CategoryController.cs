using LoginBackend.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LoginBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly MyDbContext dbContext;

        public CategoryController(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetCategories()
        {
            var categories = dbContext.Categories.ToList();
            return Ok(categories);
        }
    }
}
