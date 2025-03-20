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

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userService.GetAllUsers();
            if(users == null)
            {
                return NotFound(new Response {Message = "No hay usuarios registrados", StatusCode = 404});
            }
            
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
            var userResponse = await userService.GetUserByEmail(email);
            
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

        [HttpGet("validatePassword/{idUser}/{password}")]
        public async Task<IActionResult> ValidatePassword(int idUser, string password)
        {
            var response = await userService.ValidatePassword(idUser, password);
            if (response == "Usuario no encontrado")
            {
                return NotFound(new Response { Message = "Usuario no se encuentra registrado", StatusCode = 404 });
            }
            else if(response == "Usuario no es administrador")
            {
                return BadRequest(new Response { Message = "Usuario no es administrador", StatusCode = 400 });
            }
            else if(response == "Contraseña incorrecta, intente de nuevo")
            {
                return BadRequest(new Response { Message = "Contraseña incorrecta, intente de nuevo", StatusCode = 400 });
            }
            else if(response == "Contraseña correcta, puede continuar")
            {
                return Ok(new { Message = response, StatusCode = 200 });
            }
            return StatusCode(500, new Response { Message = "Error desconocido", StatusCode = 500 });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await userService.DeleteUser(id);
            if (user == null)
            {
                return NotFound(new Response { Message = "Usuario no se encuentra registrado", StatusCode = 404 });
            }
            return Ok(new { Message = "Usuario borrado satisfactoriamente", StatusCode = 200, DeletedUser = user });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            var updatedUser = await userService.UpdateUser(id, user);
            return Ok(new { Message = "Usuario Modificado satisfactoriamente", StatusCode = 200, UpdateUser = updatedUser });
        }

    }
}