using Microsoft.AspNetCore.Mvc;
using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Data;
using AmadeusG3_Neo_Tech_BackEnd.Services;
using AmadeusG3_Neo_Tech_BackEnd.Dtos;
using AmadeusG3_Neo_Tech_BackEnd.Mappers;

namespace AmadeusG3_Neo_Tech_BackEnd.Controllers{

    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(ApplicationDbContext dbContext)
        {
            userService = new UserService(dbContext);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("byId/{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await userService.GetUserById(id);

            if (user == null)
            {
                return NotFound(new Response {Message = "Usuario no se encuentra registrado", StatusCode = 404});
            }
            return user;
        }

        [HttpGet("byEmail/{email}")]
        public async Task<ActionResult<UserResponse>> GetUserByEmail(string email)
        {
            var user = await userService.GetUserByEmail(email);

            var userResponse = UserToUserResponse.MapUserToUserResponse(user);
            
            if (userResponse == null)
            {
                return NotFound( new Response {Message = "Usuario no se encuentra registrado", StatusCode = 404});
            }
            return userResponse;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(User user)
        {
            var newUser = await userService.CreateUser(user);
            return Created(nameof(GetUserById), newUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await userService.DeleteUser(id);
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(User user)
        {
            var updatedUser = await userService.UpdateUser(user);
            return Ok(updatedUser);
        }

    }
}