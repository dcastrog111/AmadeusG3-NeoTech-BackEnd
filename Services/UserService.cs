// Ensure that the Repositories namespace is correctly defined and referenced
using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Data;
using AmadeusG3_Neo_Tech_BackEnd.Repositories;


namespace AmadeusG3_Neo_Tech_BackEnd.Services{

    public class UserService{

        private readonly UserRepository userRepository;

        public UserService(ApplicationDbContext dbContext){
            
            userRepository = new UserRepository(dbContext);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await userRepository.GetAllUsers();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await userRepository.GetUserById(id);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await userRepository.GetUserByEmail(email);
        }

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

        public async Task<User> DeleteUser(int id)
        {
            return await userRepository.DeleteUser(id);
        }

        public async Task<User> UpdateUser(User user)
        {
            return await userRepository.UpdateUser(user);
        }

    }
}