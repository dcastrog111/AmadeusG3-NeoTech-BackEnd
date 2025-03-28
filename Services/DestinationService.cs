using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Repositories;
using AmadeusG3_Neo_Tech_BackEnd.Data;

using System.Security.Cryptography;
using System.Text;


namespace AmadeusG3_Neo_Tech_BackEnd.Services{

    public class DestinationService{

        private readonly DestinationRepository destinationRepository;

        public DestinationService(ApplicationDbContext dbContext){
            destinationRepository = new DestinationRepository(dbContext);
        }

        // Método para obtener todos los destinos
        public async Task<List<Destination>> GetAllDestinations()
        {
            return await destinationRepository.GetAllDestinations();
        }

        // Método para obtener un destino por combinación(hash)
        public async Task<Destination?> GetDestinationByCombination(string combination)
        {
            return await destinationRepository.GetDestinationByCombination(combination);
        }
        // Método para obtener un destino por id
        public async Task<Destination?> GetDestinationById(int id)
        {
            return await destinationRepository.GetDestinationById(id);
        }

        // Método para crear un destino
        public async Task<Destination?> CreateDestination(Destination destination)
        {
            return await destinationRepository.CreateDestination(destination);
        }

        // Método para obtener el hash de un destino(combinación opciones)
        public string GetDestinationHash(string id)
        {
            return ComputeSha256Hash(id);
        }

        // Método para obtener el hash de un string
        private static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}