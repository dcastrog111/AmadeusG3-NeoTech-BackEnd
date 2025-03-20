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

        public async Task<User?> DeleteUser(int id)
        {
            var user = await GetUserById(id);
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateUser(int id, User user)
        {
            var userToBeUpdate = await this.GetUserById(id);
            if(userToBeUpdate == null) return null;

            user.Id = userToBeUpdate.Id;

            var userUpdated = UpdateObject(userToBeUpdate, user);

            dbContext.Users.Update(userUpdated);
            await dbContext.SaveChangesAsync();
            return userToBeUpdate;
        }

        private static T UpdateObject<T>(T current, T newObject)
        {
            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
                var newValue = prop.GetValue(newObject);
                if(newValue == null || string.IsNullOrEmpty(newValue.ToString())){
                    continue;
                }
                prop.SetValue(current, newValue);
            }
            return current;
        }

    }
}