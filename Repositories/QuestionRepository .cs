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

        public IQueryable<Question> GetAllQuestionQuery()
        {
            return dbContext.Questions;
        }

        public async Task<List<Question>> GetAllQuestion()
        {
            return await dbContext.Questions.ToListAsync();
        }

        public async Task<Question?> GetQuestionById(int id)
        {
            return await dbContext.Questions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Question> CreateQuestion(Question question)
        {
            var newQuestion = dbContext.Questions.Add(question);
            await dbContext.SaveChangesAsync();
            return newQuestion.Entity;
        }

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