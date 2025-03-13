using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Data;
using Microsoft.EntityFrameworkCore;
using AmadeusG3_Neo_Tech_BackEnd.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// using System.Reflection;

namespace AmadeusG3_Neo_Tech_BackEnd.Repositories{
    
    public class AnswerRepository{

        private readonly ApplicationDbContext dbContext;

        public AnswerRepository(ApplicationDbContext dbContext){
            this.dbContext = dbContext;
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

        // public async Task<List<QuestionOptionCount>> GetQuestionOptionCounts()
        // {
        //     return await dbContext.Answers
        //         .GroupBy(a => a.Question_Option.Id)
        //         .Select(g => new QuestionOptionCount
        //         {
        //             QuestionOptionId = g.Key,
        //             Count = g.Count()
        //         })
        //         .ToListAsync();
        // }

        // public async Task<List<QuestionOptionCount>> GetQuestionOptionCounts()
        // {
        //     return await (from a in dbContext.Answers
        //                 join o in dbContext.Questions_Options on a.Question_Option.Id equals o.Id
        //                 select new QuestionOptionCount
        //                 {
        //                     QuestionOption = o.Description,
        //                     Count = dbContext.Answers.Count(ans => ans.Question_Option.Id == o.Id)
        //                 }).ToListAsync();
        // }
    }
}