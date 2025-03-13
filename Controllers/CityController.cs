using AmadeusG3_Neo_Tech_BackEnd.Data;
using AmadeusG3_Neo_Tech_BackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using AmadeusG3_Neo_Tech_BackEnd.Models;

namespace AmadeusG3_Neo_Tech_BackEnd.Controllers{

    [ApiController]
    [Route("api/[controller]")]
    

    public class CityController: ControllerBase{

        private readonly CityService cityService;

        public CityController(ApplicationDbContext dbContext){
            cityService = new CityService(dbContext);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCities(){
            var cities = await cityService.GetAllCities();
            return Ok(cities);
        }

        [HttpGet("byNames/{Fist_City}/{Second_City}")]

        public async Task<IActionResult> GetCityByName(string Fist_City, string Second_City){
            
            var city1 = await cityService.GetCityByName(Fist_City);

            if(city1 == null){
                return NotFound(new Response {Message = "First_City no se encuentra registrada", StatusCode = 404});
            }
            var city2 = await cityService.GetCityByName(Second_City);
            if(city2 == null){
                return NotFound(new Response {Message = "Second_City no se encuentra registrada", StatusCode = 404});
            }

            var cities = new List<City>{city1, city2};
            return Ok(cities);
        }

        [HttpGet("byId/{Id}")]
        public async Task<ActionResult<City>> GetCityById(int Id){
            var city = await cityService.GetCityById(Id);

            if (city == null){
                return NotFound(new Response {Message = "Ciudad no se encuentra registrada", StatusCode = 404});
            }
            return city;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCity(City city){

            var newCity = await cityService.GetCityByName(city.NombreDestino);
    
            if (newCity != null)
            {
                return BadRequest(new Response { Message = "La ciudad ya se encuentra registrada", StatusCode = 400 });
            }

            newCity = await cityService.CreateCity(city);
            return Created(nameof(GetCityById), newCity);
        }
    }
}