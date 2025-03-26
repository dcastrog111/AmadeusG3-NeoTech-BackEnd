using AmadeusG3_Neo_Tech_BackEnd.Config;
using AmadeusG3_Neo_Tech_BackEnd.Data;
using AmadeusG3_Neo_Tech_BackEnd.Dtos;
using AmadeusG3_Neo_Tech_BackEnd.Mappers;
using AmadeusG3_Neo_Tech_BackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using System;



namespace AmadeusG3_Neo_Tech_BackEnd.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly AuthServices _authService;

        public AuthController(ApplicationDbContext context, AuthServices authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapear el DTO a la entidad users
            var user = UserToUserResponse.MapRegisterUserDtoToUser(userDto);   

            // Agregar el usuario a la base de datos
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new { message = "Usuario registrado exitosamente." });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserDto loginDto)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == loginDto.Email);

            if (existingUser == null)
            {
                return Unauthorized(new { message = "Usuario no encontrado." });
            }

            // Verificar la contraseña encriptada
            string hashedPassword = PasswordHasher.HashPassword(loginDto.Password);

            if (existingUser.Password != hashedPassword)
            {
                return Unauthorized(new { message = "Contraseña incorrecta." });
            }

            var token = _authService.GenerateJwtToken(existingUser);
            return Ok(new { token });
        }

    }
}
