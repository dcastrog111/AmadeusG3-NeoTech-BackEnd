using Microsoft.EntityFrameworkCore;
using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Data;

namespace AmadeusG3_Neo_Tech_BackEnd.Repositories{
    
    public class QuestionRepository{

        //Instancio el contexto para usarlo en el constructor
        private readonly ApplicationDbContext dbContext;

        public QuestionRepository(ApplicationDbContext dbContext){
            this.dbContext = dbContext;
        }

        //Método para obtener todas las preguntas
        public async Task<List<Question>> GetAllQuestion()
        {
            return await dbContext.Questions.ToListAsync();
        }

        //Método para obtener una pregunta por id
        public async Task<Question?> GetQuestionById(int id)
        {
            return await dbContext.Questions.FirstOrDefaultAsync(x => x.Id == id);
        }

        //Método para crear una pregunta
        public async Task<Question> CreateQuestion(Question question)
        {
            var newQuestion = dbContext.Questions.Add(question);
            await dbContext.SaveChangesAsync();
            return newQuestion.Entity;
        }

        //Método para eliminar una pregunda por id
        public async Task<Question?> DeleteQuestion(int id)
        {
            var question = await GetQuestionById(id);
            if(question != null){
                dbContext.Questions.Remove(question);
                await dbContext.SaveChangesAsync();
            }
            return question;
        }

    }
}