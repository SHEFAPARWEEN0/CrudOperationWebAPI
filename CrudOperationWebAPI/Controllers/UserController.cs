using CrudOperationWebAPI.DataContext;
using CrudOperationWebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudOperationWebAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        
        public UserController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;          // initializes the private dappercontext
        }


        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await dbContext.UserDetails.ToListAsync());
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Getuser([FromRoute] int id)
        {
            var user = await dbContext.UserDetails.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserRequest addUserRequest)
        {
            var UserDetails = new UserDetails()
            {
                Id = new Random().Next(),                      // generate a new student id as appropriate
                FirstName = addUserRequest.FirstName,
                LastName = addUserRequest.LastName,
                
                Email = addUserRequest.Email,
                Password = addUserRequest.Password,
               
            };
            await dbContext.UserDetails.AddAsync(UserDetails);
            await dbContext.SaveChangesAsync();
            return Ok(UserDetails);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, UpdateUserRequest updateUserRequest)
        {
            var UserDetails = dbContext.UserDetails.Find(id);
            if (UserDetails != null)
            {
                UserDetails .FirstName = updateUserRequest.FirstName;
                UserDetails.LastName = updateUserRequest.LastName;

                UserDetails.Email = updateUserRequest.Email;
                UserDetails.Password = updateUserRequest.Password;
               
                await dbContext.SaveChangesAsync();
                return Ok(UserDetails);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var UserDetails = await dbContext.UserDetails.FindAsync(id);
            if (UserDetails != null)
            {
                dbContext.Remove(UserDetails);
                await dbContext.SaveChangesAsync();
                return Ok(UserDetails);
            }
            return NotFound();
        }
    }
}
