using Microsoft.AspNetCore.Mvc;
using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Data;
using AmadeusG3_Neo_Tech_BackEnd.Services;

namespace AmadeusG3_Neo_Tech_BackEnd.Controllers{

    [ApiController]
    [Route("api/[controller]")]

    public class DestinationController : ControllerBase
    {
        private readonly DestinationService destinationService;

        public DestinationController(ApplicationDbContext dbContext)
        {
            destinationService = new DestinationService(dbContext);
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetAllDestinations()
        {
            var destinations = await destinationService.GetAllDestinations();
            return Ok(destinations);
        }

        [HttpGet("{combination}")]
        public string GetDestinationByCombination(string combination)
        {
            var hash = destinationService.GetDestinationHash(combination);
    
            return hash;
        }
        
        [HttpPost("create")]
        public async Task<ActionResult> CreateDestination(Destination destination)
        {
            var newDestination = await destinationService.CreateDestination(destination);
            if(newDestination == null)
            {
                return BadRequest(new Response {Message = "Error al crear destino", StatusCode = 400});
            }

            return Ok(newDestination);
        }
    }

}