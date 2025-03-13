using Microsoft.EntityFrameworkCore;
using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Data;
using System.Reflection;

// using System.Reflection;

namespace AmadeusG3_Neo_Tech_BackEnd.Repositories{
    
    public class UserRepository{

        //Instancio el contexto para usarlo en el constructor
        private readonly ApplicationDbContext dbContext;

        public UserRepository(ApplicationDbContext dbContext){
            this.dbContext = dbContext;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await dbContext.Users.ToListAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User> CreateUser(User user)
        {
            var newUser = dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            return newUser.Entity;
        }

        public async Task<User> DeleteUser(int id)
        {
            var user = await GetUserById(id);
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            var userToUpdate = await GetUserById(user.Id);
            if (userToUpdate == null) return null;

            userToUpdate.Full_Name = user.Full_Name;
            userToUpdate.Email = user.Email;
            userToUpdate.Password = user.Password;
            userToUpdate.Tipo_Usuario = user.Tipo_Usuario;


            dbContext.Users.Update(userToUpdate);
            await dbContext.SaveChangesAsync();
            return userToUpdate;
        }

    }
}