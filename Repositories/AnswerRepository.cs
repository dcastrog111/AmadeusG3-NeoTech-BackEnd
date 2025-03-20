using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Data;
using Microsoft.EntityFrameworkCore;
using AmadeusG3_Neo_Tech_BackEnd.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO.Compression;

// using System.Reflection;

namespace AmadeusG3_Neo_Tech_BackEnd.Repositories{
    
    public class AnswerRepository{

        private readonly ApplicationDbContext dbContext;

        public AnswerRepository(ApplicationDbContext dbContext){
            this.dbContext = dbContext;
        }

        public IQueryable<Answer> GetAllAnswersQuery()
        {
            return dbContext.Answers;
        }

        public Answer GetByIdQuery(int Id)
        {
            return dbContext.Answers.FirstOrDefault(answer => answer.Id == Id);
        }

        public async Task<List<Answer>> GetAllAnswers()
        {
            return await dbContext.Answers.ToListAsync();
        }

        public async Task<Answer?> GetAnswerById(int Id)
        {
            return await dbContext.Answers.FirstOrDefaultAsync(answer => answer.Id == Id);
        }

        public async Task<List<Answer>> GetAnswersByUserId(int userId)
        {
            return await dbContext.Answers.Include(u => u.User).Include(o => o.Question_Option).Include(q => q.Question)
                .Where(answer => answer.User.Id == userId).ToListAsync();
        }

        public async Task<List<Answer>> GetAnswersByQuestionId(int questionId)
        {
            return await dbContext.Answers.Include(q => q.Question).Where(answer => answer.Question.Id == questionId).ToListAsync();
        }

        public async Task<List<Answer>> GetAnswersByUserIdQuestionId(int userId, int questionId) 
        {
            return await dbContext.Answers.Include(q => q.Question).Include(u => u.User).Include(o => o.Question_Option)
                .Where(answer => answer.Question.Id == questionId).Where(answer => answer.User.Id == userId).ToListAsync();
        }

        public async Task<Answer> CreateAnswer(Answer answer)
        {
            var newAnswer = dbContext.Answers.Add(answer);
            await dbContext.SaveChangesAsync();
            return newAnswer.Entity;
        }

        public async Task<Answer> UpdateAnswer(Answer answer)
        {
            dbContext.Answers.Update(answer);
            await dbContext.SaveChangesAsync();
            return answer;
        }

        public async Task<List<QuestionOptionCount>> GetQuestionOptionCounts()
        {
            var query = from a in dbContext.Answers
                        join qo in dbContext.Questions_Options
                        on a.Question_Option.Id equals qo.Id

                        //Renombrar porque no se puede tener dos campos con el mismo nombre, asi este precedidos por la tabla a la que pertenecen
                        group new {a.Question.Id, QuestionOptionId = qo.Id, qo.Description } 
                        by new  {a.Question.Id, QuestionOptionId = qo.Id, qo.Description } into g
                        select new
                        {
                            Question_OptionId = g.Key.QuestionOptionId,
                            Count = g.Count(),
                            Description = g.Key.Description
                        };

            return await query.Select(q => new QuestionOptionCount
            {
                QuestionOptionText = q.Description,
                Count = q.Count
            }).ToListAsync();

        }

        public async Task<List<AnswerDescriptionUser>> GetAnswerTextByUser(int userId)
        {
            var query = from a in dbContext.Answers
                        join qo in dbContext.Questions_Options
                        on a.Question_Option.Id equals qo.Id
                        join q in dbContext.Questions
                        on a.Question.Id equals q.Id
                        where a.User.Id == userId
                        select new
                        {
                            q.Question_Text,
                            qo.Description
                        };

            return await query.Select(q => new AnswerDescriptionUser
            {
                QuestionText = q.Question_Text,
                QuestionOptionText = q.Description
                
            }).ToListAsync();
        }
    }

}