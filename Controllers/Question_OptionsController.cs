using Microsoft.AspNetCore.Mvc;
using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Data;
using AmadeusG3_Neo_Tech_BackEnd.Services;
using AmadeusG3_Neo_Tech_BackEnd.Dtos;

namespace AmadeusG3_Neo_Tech_BackEnd.Controllers{
    
    [ApiController]
    [Route("api/[controller]")]

    public class Question_OptionsController : ControllerBase
    {
        private readonly Question_OptionsService question_OptionsService;

        public Question_OptionsController(ApplicationDbContext dbContext)
        {
            question_OptionsService = new Question_OptionsService(dbContext);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllQuestion_Options()
        {
            var question_Options = await question_OptionsService.GetAllQuestion_Options();
            if(question_Options.Count == 0)
            {
                return NotFound(new Response {Message = "No hay opciones de preguntas registradas", StatusCode = 404});
            }
            
            return Ok(question_Options);
        }

        [HttpGet("byId/{id}")]
        public async Task<ActionResult<Question_Options?>> GetQuestion_OptionsById(int id)
        {
            var question_Options = await question_OptionsService.GetQuestion_OptionsById(id);

            if (question_Options == null)
            {
                return NotFound(new Response {Message = "Opcion de pregunta no se encuentra registrada", StatusCode = 404});
            }
            return question_Options;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateQuestion_Options(Question_OptionsRequest question_OptionsRequest)
        {
            var newQuestion_Options = await question_OptionsService.CreateQuestion_Options(question_OptionsRequest);

            if(newQuestion_Options == null)
            {
                return BadRequest(new Response {Message = "No se pudo crear la opcion de pregunta", StatusCode = 400});
            }
            
            return Created(nameof(GetQuestion_OptionsById), newQuestion_Options);
        }

        [HttpGet("byQuestionId/{questionId}")]
        public async Task<IActionResult> GetOptionsByQuestionId(int questionId)
        {
            var question_Options = await question_OptionsService.GetOptionsByQuestionId(questionId);
            if(question_Options.Count == 0)
            {
                return NotFound(new Response {Message = "No hay opciones de preguntas registradas", StatusCode = 404});
            }
            
            return Ok(question_Options);
        }

        [HttpGet("byDescription/{description}")]
        public async Task<IActionResult> GetQuestion_OptionsByDescription(string description)
        {
            var question_Options = await question_OptionsService.GetQuestion_OptionsByDescription(description);

            if (question_Options == null)
            {
                return NotFound(new Response {Message = "Opcion de pregunta no se encuentra registrada", StatusCode = 404});
            }
            return Ok(question_Options);
        }
    }

}