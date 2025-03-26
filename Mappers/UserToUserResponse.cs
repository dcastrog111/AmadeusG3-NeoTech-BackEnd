using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Dtos;
using AmadeusG3_Neo_Tech_BackEnd.Config;

namespace AmadeusG3_Neo_Tech_BackEnd.Mappers
{
    public class UserToUserResponse
    {
        public static UserResponse MapUserToUserResponse (User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Full_Name = user.Full_Name,
                Email = user.Email,
                Tipo_Usuario = (int)user.Tipo_Usuario
            };
        }

        public static User MapRegisterUserDtoToUser(RegisterUserDto userDto)
        {
            return new User
            {
                
                Full_Name = userDto.Full_Name,
                Email = userDto.Email,
                Password = PasswordHasher.HashPassword(userDto.Password),
                Tipo_Usuario = Tipo_Usuario.cliente
            };
        }
    }
}