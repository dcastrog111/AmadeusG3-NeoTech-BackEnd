using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Data;
using Microsoft.EntityFrameworkCore;

namespace AmadeusG3_Neo_Tech_BackEnd.Repositories{

    public class DestinationRepository{

        private readonly ApplicationDbContext dbContext;

        public DestinationRepository(ApplicationDbContext dbContext){
            this.dbContext = dbContext;
        }

        // Método para obtener todos los destinos
        public async Task<List<Destination>> GetAllDestinations()
        {
            return await dbContext.Destinations.ToListAsync();
        }

        // Método para obtener un destino por combinación(hash)
        public async Task<Destination?> GetDestinationByCombination(string combination)
        {
            return await dbContext.Destinations.FirstOrDefaultAsync(Destination => Destination.Combination == combination);
        }

        // Método para obtener un destino por id
        public async Task<Destination?> GetDestinationById(int id)
        {
            return await dbContext.Destinations.FirstOrDefaultAsync(Destination => Destination.Id == id);
        }

        //Método para crear un destino
        public async Task<Destination?> CreateDestination(Destination destination)
        {
            var newDestination = dbContext.Destinations.Add(destination);
            await dbContext.SaveChangesAsync();
            return newDestination.Entity;
        }
    }
}