using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Core.IConfiguration;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> logger;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(ILogger<UserController> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult("Something is wrong") { StatusCode = 500 };
            }
            user.Id = Guid.NewGuid();

            await _unitOfWork.Users.Add(user);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction("GetItem", new { user.Id }, user);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetItem(Guid id)
        {
            var user = _unitOfWork.Users.GetById(id: id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult> Get(Guid id)
        {
            var users = await _unitOfWork.Users.All();

            if (users != null && users.Any())
            {
                return Ok(users);
            }
            return NotFound(); 
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(User entity)
        {
            await _unitOfWork.Users.Upsert(entity);
            await _unitOfWork.CompleteAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(Guid id)
        {
            var user = await _unitOfWork.Users.GetById(id);
            if (user == null) 
            {
                return BadRequest();
            }

            await _unitOfWork.Users.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(user);

        }
    }
}
