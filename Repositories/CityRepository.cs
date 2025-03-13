using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Data;
using Microsoft.EntityFrameworkCore;

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

    }
}