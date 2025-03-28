// Ensure that the Repositories namespace is correctly defined and referenced
using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Data;
using AmadeusG3_Neo_Tech_BackEnd.Repositories;
using AmadeusG3_Neo_Tech_BackEnd.Dtos;
using AmadeusG3_Neo_Tech_BackEnd.Mappers;


namespace AmadeusG3_Neo_Tech_BackEnd.Services{

    public class UserService{

        private readonly UserRepository userRepository;

        public UserService(ApplicationDbContext dbContext){
            
            userRepository = new UserRepository(dbContext);
        }

        //Metodo para obtener todos los usuarios
        //Se retorna el DTO UserResponse para no exponer la contraseña del usuario
        public async Task<List<UserResponse>> GetAllUsers()
        {
            var users = await userRepository.GetAllUsers();
            var userResponse = new List<UserResponse>();
            foreach (var user in users)
            {
                userResponse.Add(UserToUserResponse.MapUserToUserResponse(user)) ;
            }
            return userResponse;
        }

        //Metodo para obtener un usuario por id
        public async Task<User?> GetUserById(int id)
        {
            return await userRepository.GetUserById(id);
        }

        //Metodo para obtener un usuario por Email
        //Se retorna el DTO UserResponse para no exponer la contraseña del usuario
        public async Task<UserResponse?> GetUserByEmail(string email)
        {
            var user = await userRepository.GetUserByEmail(email);
            if(user == null) return null;
            var userResponse = UserToUserResponse.MapUserToUserResponse(user);
            return userResponse; 
        }

        //Metodo para crear un usuario
        //Se valida con el email usuario ya existe, si no existe se crea el usuario
        public async Task<User> CreateUser(User user)
        {
            var saveUser = await userRepository.GetUserByEmail(user.Email);
            if(saveUser == null){
                return await userRepository.CreateUser(user);
            }
            else{
                return saveUser;
            }
        }

        //Metodo para validar la contraseña del usuario administrador
        //Se valida que el usuario exista y que sea administrador
        public async Task<string> ValidatePassword(int idUser, string password)
        {
            var user = await userRepository.GetUserById(idUser);
            if(user == null) return "Usuario no encontrado";
            if(user.Tipo_Usuario == 0) return "Usuario no es administrador";
            if (user.Password == password) return "Contraseña correcta, puede continuar";
            else return "Contraseña incorrecta, intente de nuevo";
        }

        //Metodo para validar eliminar un usuario
        public async Task<User?> DeleteUser(int id)
        {
            return await userRepository.DeleteUser(id);
        }

        //Metodo para validar actualizar un usuario
        public async Task<User?> UpdateUser(int id, User user)
        {
            return await userRepository.UpdateUser(id, user);
        }

    }
}