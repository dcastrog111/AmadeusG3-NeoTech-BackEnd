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

        public async Task<List<Destination>> GetAllDestinations()
        {
            return await destinationRepository.GetAllDestinations();
        }

        public async Task<Destination?> GetDestinationByCombination(string combination)
        {
            return await destinationRepository.GetDestinationByCombination(combination);
        }

        public async Task<Destination?> GetDestinationById(int id)
        {
            return await destinationRepository.GetDestinationById(id);
        }

        public async Task<Destination?> CreateDestination(Destination destination)
        {
            return await destinationRepository.CreateDestination(destination);
        }

        public string GetDestinationHash(string id)
        {
            return ComputeSha256Hash(id);
        }

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