using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Data;
using AmadeusG3_Neo_Tech_BackEnd.Repositories;


namespace AmadeusG3_Neo_Tech_BackEnd.Services{

    public class QuestionService{

        private readonly QuestionRepository questionRepository;

        public QuestionService(ApplicationDbContext dbContext){
            
            questionRepository = new QuestionRepository(dbContext);
        }

        // Método para obtener todas las preguntas
        public async Task<List<Question>> GetAllQuestion()
        {
            var questions = questionRepository.GetAllQuestion();

            return await questions;
        }

        // Método para obtener una pregunta por id
        public async Task<Question?> GetQuestionById(int id)
        {
            return await questionRepository.GetQuestionById(id);
        }

        // Método para crear una pregunta
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