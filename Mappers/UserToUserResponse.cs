using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Dtos;

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
                Tipo_Usuario = (int)user.Tipo_Usuario,
                Avatar = user.Avatar
            };
        }
    }
}