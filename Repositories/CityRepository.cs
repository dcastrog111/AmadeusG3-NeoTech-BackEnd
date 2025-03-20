using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AmadeusG3_Neo_Tech_BackEnd.Repositories{

    public class CityRepository{
        
        private readonly ApplicationDbContext dbContext;

        public CityRepository(ApplicationDbContext dbContext){
            this.dbContext = dbContext;
        }

        public async Task<List<City>> GetAllCities()
        {
            return await dbContext.Cities.ToListAsync();
        }

        public async Task<City?> GetCityByName(string NombreDestino)
        {
            return await dbContext.Cities.FirstOrDefaultAsync(city => city.NombreDestino == NombreDestino);
        }

        public async Task<City?> GetCityById(int Id)
        {
            return await dbContext.Cities.FirstOrDefaultAsync(city => city.Id == Id);
        }

        public async Task<City> CreateCity(City city)
        {
            var newCity = dbContext.Cities.Add(city);
            await dbContext.SaveChangesAsync();
            return newCity.Entity;
        }

        public async Task<City?> UpdateUser(int id, City city)
        {
            var cityToBeUpdate = await this.GetCityById(id);
            if(cityToBeUpdate == null) return null;

            city.Id = cityToBeUpdate.Id;

            var cityUpdated = UpdateObject(cityToBeUpdate, city);

            dbContext.Cities.Update(cityUpdated);
            await dbContext.SaveChangesAsync();
            return cityToBeUpdate;
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