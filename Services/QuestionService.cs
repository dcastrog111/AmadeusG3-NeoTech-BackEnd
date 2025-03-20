using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Data;
using AmadeusG3_Neo_Tech_BackEnd.Repositories;


namespace AmadeusG3_Neo_Tech_BackEnd.Services{

    public class QuestionService{

        private readonly QuestionRepository questionRepository;

        public QuestionService(ApplicationDbContext dbContext){
            
            questionRepository = new QuestionRepository(dbContext);
        }

        public async Task<List<Question>> GetAllQuestion()
        {
            var questions = questionRepository.GetAllQuestion();

            return await questions;
        }

        public async Task<Question?> GetQuestionById(int id)
        {
            return await questionRepository.GetQuestionById(id);
        }

        public async Task<Question> CreateQuestion(Question question)
        {
            var saveQuestion = await questionRepository.CreateQuestion(question);
            if(saveQuestion == null){
                return await questionRepository.CreateQuestion(question);
            }
            else{
                return saveQuestion;
            }
        }
    }
}