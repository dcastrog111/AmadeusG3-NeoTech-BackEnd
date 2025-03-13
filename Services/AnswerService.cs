using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Data;
using AmadeusG3_Neo_Tech_BackEnd.Repositories;
using AmadeusG3_Neo_Tech_BackEnd.Dtos;
using AmadeusG3_Neo_Tech_BackEnd.Mappers;


namespace AmadeusG3_Neo_Tech_BackEnd.Services{

    public class AnswerService{

        private readonly AnswerRepository answerRepository;
        private readonly UserRepository userRepository;
        private readonly QuestionRepository questionRepository;
        private readonly Question_OptionsRepository question_OptionsRepository;
        private readonly AnswerResponse answerResponse;
        private readonly DestinationService destinationService;
        private readonly CityService cityService;

        public AnswerService(ApplicationDbContext dbContext){
            
            answerRepository = new AnswerRepository(dbContext);
            userRepository = new UserRepository(dbContext);
            questionRepository = new QuestionRepository(dbContext);
            question_OptionsRepository = new Question_OptionsRepository(dbContext);
            answerResponse = new AnswerResponse();
            destinationService = new DestinationService(dbContext);
            cityService = new CityService(dbContext);
        }

        public async Task<Answer?> CreateAnswer(AnswerRequest answerRequest)
        {
            var user = await userRepository.GetUserById(answerRequest.UserId);

            var question = await questionRepository.GetQuestionById(answerRequest.QuestionId);

            var questionOption = await question_OptionsRepository.GetQuestion_OptionsById(answerRequest.QuestionOptionId);

            Answer newAnswer = AnswerRequestToAnswer.MapAnswerRequestToAnswer(user, question, questionOption);

            var pastAnswer = await answerRepository.GetAnswersByUserIdQuestionId(answerRequest.UserId, answerRequest.QuestionId);


            if(pastAnswer.Count == 0)
            {
                var createdAnswer = await answerRepository.CreateAnswer(newAnswer);
                return createdAnswer;
            }
            return null;

        }

        public async Task<AnswerResponse?> GetCitiesByUserId(int userId)
        {
            var answers = await answerRepository.GetAnswersByUserId(userId);
            var combination = "";
            foreach (var answer in answers)
            {
                var questionOptionId = answer.Question_Option.Id;

                if(combination == "")
                {
                    combination = questionOptionId.ToString();
                    continue;
                }
                combination = combination + "-" + questionOptionId;
                
            }

            var hash = destinationService.GetDestinationHash(combination);
            var destination = await destinationService.GetDestinationByCombination(hash);

            AnswerResponse answerResponse = new AnswerResponse();
            answerResponse.First_City = cityService.GetCityById(destination.First_City_Id).Result.NombreDestino;
            answerResponse.Second_City = cityService.GetCityById(destination.Second_City_Id).Result.NombreDestino;

            return answerResponse;
            
        }

        public async Task<List<Answer>> GetAnswersByUserIdQuestionId(int userId, int questionId)
        {
            return await answerRepository.GetAnswersByUserIdQuestionId(userId, questionId);
        }

    }
}