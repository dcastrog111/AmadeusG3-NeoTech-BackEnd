using AmadeusG3_Neo_Tech_BackEnd.Data;
using AmadeusG3_Neo_Tech_BackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Dtos;

namespace AmadeusG3_Neo_Tech_BackEnd.Controllers{
    
    
    [ApiController]
    [Route("api/[controller]")]

    public class AnswerController : ControllerBase{

        private readonly AnswerService answerService;

        public AnswerController(ApplicationDbContext dbContext){
            answerService = new AnswerService(dbContext);
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateAnswer(AnswerRequest answerRequest){
            
            Answer? answer = await answerService.CreateAnswer(answerRequest);

            if(answer == null)
            {
                return BadRequest(new Response {Message = "Usuario ya ha registrado esta opción", StatusCode = 400});
            }
            return Ok(answer);
        }

        [HttpGet("{userId}/{questionId}")]
        public async Task<IActionResult> GetAnswersByUserIdQuestionId(int userId, int questionId)
        {
            var answers = await answerService.GetAnswersByUserIdQuestionId(userId, questionId);
            if(answers.Count == 0)
            {
                return NotFound(new Response {Message = "Usuario no tiene categorías registradas o Usuario no tiene registrada esta categoría", StatusCode = 404});
            }

            return Ok(answers);
        }

        [HttpGet("destinosByUserId/{userId}")]
        public async Task<IActionResult> GetCitiesByUserId(int userId)
        {
            var answerResponse = await answerService.GetCitiesByUserId(userId);
            return Ok(answerResponse);
        }

        [HttpGet("byUserId/{userId}")]
        public async Task<IActionResult> GetAnswersByUserId(int userId)
        {
            var answers = await answerService.GetAnswersByUserId(userId);
            if(answers.Count == 0)
            {
                return NotFound(new Response {Message = "Usuario no tiene opciones registradas", StatusCode = 404});
            }

            return Ok(answers);
        }

        // [HttpGet("CountAnswers")]
        // public async Task<IActionResult> GetQuestionOptionCounts()
        // {
        //     var questionOptionCounts = await answerService.GetQuestionOptionCounts();
        //     return Ok(questionOptionCounts);
        // }

    }
}