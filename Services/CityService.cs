using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Data;
using AmadeusG3_Neo_Tech_BackEnd.Repositories;

namespace AmadeusG3_Neo_Tech_BackEnd.Services{

    public class CityService{

        private readonly CityRepository cityRepository;

        public CityService(ApplicationDbContext dbContext){
            
            cityRepository = new CityRepository(dbContext);
        }
        
        //Método para obtener todas las ciudades
        public async Task<List<City>> GetAllCities()
        {
            return await cityRepository.GetAllCities();
        }

        //Método para obtener una ciudad por su nombre
        public async Task<City?> GetCityByName(string NombreDestino)
        {
            return await cityRepository.GetCityByName(NombreDestino);
        }

        //Método para obtener una ciudad por su id
        public async Task<City?> GetCityById(int Id)
        {
            return await cityRepository.GetCityById(Id);
        }

        //Método para crear una ciudad
        public async Task<City> CreateCity(City city)
        {
            return await cityRepository.CreateCity(city);
        }

        //Método para actualizar una ciudad
        public async Task<City?> UpdateUser(int id, City city)
        {
            return await cityRepository.UpdateUser(id, city);
        }

    }
}