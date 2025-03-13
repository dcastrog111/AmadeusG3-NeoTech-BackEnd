using Microsoft.AspNetCore.Mvc;
using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Data;
using AmadeusG3_Neo_Tech_BackEnd.Services;

namespace AmadeusG3_Neo_Tech_BackEnd.Controllers{

    [ApiController]
    [Route("api/[controller]")]

    public class QuestionController : ControllerBase
    {
        private readonly QuestionService questionService;

        public QuestionController(ApplicationDbContext dbContext)
        {
            questionService = new QuestionService(dbContext);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllQuestions()
        {
            var questions = await questionService.GetAllQuestion();
            if(questions == null)
            {
                return NotFound(new Response {Message = "No hay preguntas registradas", StatusCode = 404});
            }
            
            return Ok(questions);
        }

        [HttpGet("byId/{id}")]
        public async Task<ActionResult<Question>> GetQuestionById(int id)
        {
            var question = await questionService.GetQuestionById(id);

            if (question == null)
            {
                return NotFound(new Response {Message = "Pregunta no se encuentra registrada", StatusCode = 404});
            }
            return question;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateQuestion(Question question)
        {
            var newQuestion = await questionService.CreateQuestion(question);

            if(newQuestion == null)
            {
                return BadRequest(new Response {Message = "No se pudo crear la pregunta", StatusCode = 400});
            }
            
            return Created(nameof(GetQuestionById), newQuestion);
        }
    }


}