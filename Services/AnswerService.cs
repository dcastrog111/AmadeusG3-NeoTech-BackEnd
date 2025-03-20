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


        public List<QuestionOptionCount> ReporteQuestionsByCount()
        {
            IQueryable<Answer> answers = answerRepository.GetAllAnswersQuery();

            IQueryable<Question_Options> questionOptions = question_OptionsRepository.GetAllQuestion_OptionsQuery();

            List<QuestionOptionCount> query = answers.GroupBy(x => x.Question_Option.Id)
                .Select(x => new QuestionOptionCount
                {
                    QuestionOptionText = x.First().Question_Option.Description,
                    Count = x.Count()
                })
                .OrderByDescending(x => x.Count)
                .ToList();

            Console.WriteLine(query);
            
            return query;
        }

        public async Task<AnswerResponse?> SaveAndCalculate(int userId, string respuestas)
        {
            User user = await userRepository.GetUserById(userId);
            if (user is null) return null;

            var respuestasArray = respuestas.Split("-");
            var i = 1;

            foreach (var item in respuestasArray)
            {
                Question question = await questionRepository.GetQuestionById(i);
                if (question is null) return null;

                Question_Options questionOption = await question_OptionsRepository.GetQuestion_OptionsByDescription(item);
                if (questionOption is null) return null;

                Answer newAnswer = AnswerRequestToAnswer.MapAnswerRequestToAnswer(user, question, questionOption);

                await answerRepository.CreateAnswer(newAnswer);
                i++;
            }

            var hash = destinationService.GetDestinationHash(respuestas);

            Destination destination = await destinationService.GetDestinationByCombination(hash);


            AnswerResponse answerResponse = new AnswerResponse();

            if(destination is null){
                answerResponse.First_City = "Bora Bora";
                answerResponse.Second_City = "Dubái";

                return answerResponse;
            }

            answerResponse.First_City = cityService.GetCityById(destination.First_City_Id).Result.NombreDestino;
            answerResponse.Second_City = cityService.GetCityById(destination.Second_City_Id).Result.NombreDestino;

            Console.WriteLine(answerResponse.First_City);
            Console.WriteLine(answerResponse.Second_City);

            return answerResponse;
        }

        public async Task<AnswerResponse?> GetCitiesByUserId(int userId)
        {
            //List<int> answers1 = answerRepository.GetAllAnswersQuery().Select(x => x.Question_Option.Id).ToList();
            var answers = await answerRepository.GetAnswersByUserId(userId);
            string combination = string.Join("-",answers);

            var hash = destinationService.GetDestinationHash(combination);
            var destination = await destinationService.GetDestinationByCombination(hash);

            AnswerResponse answerResponse = new AnswerResponse();

            if(destination is null){
                answerResponse.First_City = "Bora Bora";
                answerResponse.Second_City = "Dubái";

                return answerResponse;
            }

            answerResponse.First_City = cityService.GetCityById(destination.First_City_Id).Result.NombreDestino;
            answerResponse.Second_City = cityService.GetCityById(destination.Second_City_Id).Result.NombreDestino;

            return answerResponse;    
        }

        public async Task<List<Answer>> GetAnswersByUserIdQuestionId(int userId, int questionId)
        {
            return await answerRepository.GetAnswersByUserIdQuestionId(userId, questionId);
        }

        public async Task<List<Answer>> GetAnswersByUserId(int userId)
        {
            return await answerRepository.GetAnswersByUserId(userId);
        }

        public async Task<List<QuestionOptionCount>> GetQuestionOptionCounts()
        {
            return await answerRepository.GetQuestionOptionCounts();
        }

        public async Task<List<AnswerDescriptionUser>> GetAnswerTextByUser(int userId)
        {
            return await answerRepository.GetAnswerTextByUser(userId);
        }

    }
}