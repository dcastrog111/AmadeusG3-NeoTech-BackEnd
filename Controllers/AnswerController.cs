using AmadeusG3_Neo_Tech_BackEnd.Data;
using AmadeusG3_Neo_Tech_BackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace AmadeusG3_Neo_Tech_BackEnd.Controllers{
    
    
    [ApiController]
    [Route("api/[controller]")]

    public class AnswerController : ControllerBase{

        private readonly AnswerService answerService;

        public AnswerController(ApplicationDbContext dbContext){
            answerService = new AnswerService(dbContext);
        }

        //Endpoint para guardar las respuestas y calcular el destino
        [HttpGet("get/{userId}/{respuestas}")]
        public async Task<IActionResult> SaveAndCalculate(int userId, string respuestas)
        {
            var answerResponse = await answerService.SaveAndCalculate(userId, respuestas);
            return Ok(answerResponse);
        }

        //Endpoint para obtener todas las respuestas de un usuario - para reportes
        [HttpGet("CountAnswers")]
        public async Task<IActionResult> GetQuestionOptionCounts()
        {
            var questionOptionCounts = await answerService.GetQuestionOptionCounts();
            return Ok(questionOptionCounts);
        }

        //Endpoint para obtener todas respuestas por id de pregunta - para reportes
        [HttpGet("CountAnswersById/{questionId}")]
        public async Task<IActionResult> GetQuestionOptionCountsById(int questionId)
        {
            var questionOptionCounts = await answerService.GetQuestionOptionCountsById(questionId);
            return Ok(questionOptionCounts);
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

    }
}