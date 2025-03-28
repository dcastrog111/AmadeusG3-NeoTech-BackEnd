using Microsoft.EntityFrameworkCore;
using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Data;
using System.ComponentModel;

namespace AmadeusG3_Neo_Tech_BackEnd.Repositories{
    
    public class Question_OptionsRepository{

        
        private readonly ApplicationDbContext dbContext;

        public Question_OptionsRepository(ApplicationDbContext dbContext){
            this.dbContext = dbContext;
        } 

        //Método para obtener todas las opciones de preguntas e incluir la pregunta a la que pertenecen
        public async Task<List<Question_Options>> GetAllQuestion_Options()
        {
            return await dbContext.Questions_Options.Include(q => q.Question).ToListAsync();
        
        }

        //Método para obtener una opción de pregunta por id e incluir la pregunta a la que pertenece
        public async Task<Question_Options?> GetQuestion_OptionsById(int id)
        {
            return await dbContext.Questions_Options.Include(q => q.Question).FirstOrDefaultAsync(question_options => question_options.Id == id);
        }

        //Método para obtener una opción de pregunta por descripción e incluir la pregunta a la que pertenece
        public async Task<Question_Options?> GetQuestion_OptionsByDescription(string description)
        {
            return await dbContext.Questions_Options.Include(q => q.Question).FirstOrDefaultAsync(question_options => question_options.Description == description);
        }

        //Método para crear una opción de pregunta
        public async Task<Question_Options> CreateQuestion_Options(Question_Options question_options)
        {
            var newQuestion_options = dbContext.Questions_Options.Add(question_options);
            await dbContext.SaveChangesAsync();
            return newQuestion_options.Entity;
        }

        //Método para obtener todas las opciones de una pregunta por el id de la pregunta a la que pertenecen
        public async Task<List<Question_Options>> GetOptionsByQuestionId(int questionId)
        {
            return await dbContext.Questions_Options.Where(Question_Options => Question_Options.Question.Id == questionId).ToListAsync();
        }

    }
}