using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Data;
using Microsoft.EntityFrameworkCore;
using AmadeusG3_Neo_Tech_BackEnd.Dtos;

// using System.Reflection;

namespace AmadeusG3_Neo_Tech_BackEnd.Repositories{
    
    public class AnswerRepository{

        private readonly ApplicationDbContext dbContext;

        public AnswerRepository(ApplicationDbContext dbContext){
            this.dbContext = dbContext;
        }

        //Método para obtener todas las respuestas
        public async Task<List<Answer>> GetAllAnswers()
        {
            return await dbContext.Answers.ToListAsync();
        }

        //Método para obtener una respuesta por id
        public async Task<Answer?> GetAnswerById(int Id)
        {
            return await dbContext.Answers.FirstOrDefaultAsync(answer => answer.Id == Id);
        }

        //Método para obtener una respuesta por el id del usuario, incluyendo la pregunta y la opción de respuesta
        public async Task<List<Answer>> GetAnswersByUserId(int userId)
        {
            return await dbContext.Answers.Include(u => u.User).Include(o => o.Question_Option).Include(q => q.Question)
                .Where(answer => answer.User.Id == userId).ToListAsync();
        }

        //Método para obtener una respuesta por el id de la pregunta, incluyendo la pregunta
        public async Task<List<Answer>> GetAnswersByQuestionId(int questionId)
        {
            return await dbContext.Answers.Include(q => q.Question).Where(answer => answer.Question.Id == questionId).ToListAsync();
        }

        //Método para obtener una respuesta por el id del usuario y el id de la pregunta, incluyendo la pregunta y la opción de respuesta
        public async Task<List<Answer>> GetAnswersByUserIdQuestionId(int userId, int questionId) 
        {
            return await dbContext.Answers.Include(q => q.Question).Include(u => u.User).Include(o => o.Question_Option)
                .Where(answer => answer.Question.Id == questionId).Where(answer => answer.User.Id == userId).ToListAsync();
        }

        //Método para crear una respuesta
        public async Task<Answer> CreateAnswer(Answer answer)
        {
            var newAnswer = dbContext.Answers.Add(answer);
            await dbContext.SaveChangesAsync();
            return newAnswer.Entity;
        }

        //Método para actualiza una respuesta
        public async Task<Answer> UpdateAnswer(Answer answer)
        {
            dbContext.Answers.Update(answer);
            await dbContext.SaveChangesAsync();
            return answer;
        }

        //Método para contar las opciones seleccionadas por los usuarios y guardas en la tabla answers
        //Retorna una lista de objetos QuestionOptionCount que contiene el texto de la opción de respuesta, el conteo, el id y el texto de la pregunta
        public async Task<List<QuestionOptionCount>> GetQuestionOptionCounts()
        {
            var query = from a in dbContext.Answers
                        join qo in dbContext.Questions_Options
                        on a.Question_Option.Id equals qo.Id
                        join q in dbContext.Questions
                        on a.Question.Id equals q.Id

                        //Renombrar porque no se puede tener dos campos con el mismo nombre, asi este precedidos por la tabla a la que pertenecen
                        group new {a.Question.Id, QuestionOptionId = qo.Id, qo.Description, q.Question_Text } 
                        by new  {a.Question.Id, QuestionOptionId = qo.Id, qo.Description, q.Question_Text } into g
                        select new
                        {
                            Question_OptionId = g.Key.QuestionOptionId,
                            Count = g.Count(),
                            Description = g.Key.Description,
                            QuestionText = g.Key.Question_Text,
                            QuestionId = g.Key.Id
                        };

            return await query.Select(q => new QuestionOptionCount
            {
                QuestionOptionText = q.Description,
                Count = q.Count,
                QuestionText = q.QuestionText,
                Question = q.QuestionId
            }).ToListAsync();

        }

        //Método para contar las opciones seleccionadas por los usuarios para cada pregunta y guardas en la tabla answers
        //Retorna una lista de objetos QuestionOptionCount que contiene el texto de la opción de respuesta, el conteo, el id y el texto de la pregunta
        public async Task<List<QuestionOptionCount>> GetQuestionOptionCountsById(int QuestionId)
        {
            var query = from a in dbContext.Answers
                        join qo in dbContext.Questions_Options
                        on a.Question_Option.Id equals qo.Id
                        join q in dbContext.Questions
                        on a.Question.Id equals q.Id

                        //Renombrar porque no se puede tener dos campos con el mismo nombre, asi este precedidos por la tabla a la que pertenecen
                        group new {a.Question.Id, QuestionOptionId = qo.Id, qo.Description, q.Question_Text } 
                        by new  {a.Question.Id, QuestionOptionId = qo.Id, qo.Description, q.Question_Text } into g
                        where g.Key.Id == QuestionId
                        select new
                        {
                            Question_OptionId = g.Key.QuestionOptionId,
                            Count = g.Count(),
                            Description = g.Key.Description,
                            QuestionText = g.Key.Question_Text,
                            QuestionId = g.Key.Id
                        };

            return await query.Select(q => new QuestionOptionCount
            {
                QuestionOptionText = q.Description,
                Count = q.Count,
                QuestionText = q.QuestionText,
                Question = q.QuestionId
            }).ToListAsync();

        }

        //Método para obtener las opciones seleccionadas por un usuario y guardas en la tabla answers
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