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

        //Método para guardar las respuestas y calcular el destino
        //Recibe el id del usuario y una cadena de respuestas separadas por guiones
        public async Task<AnswerResponse?> SaveAndCalculate(int userId, string respuestas)
        {
            //Validar y obtener el objeto User asociado a la respuesta
            User user = await userRepository.GetUserById(userId);
            if (user is null) return null;

            //convertir la cadena de respuestas en un array de strings
            var respuestasArray = respuestas.Split("-");
            var i = 1;

            //Recorrer el array de respuestas y crear una respuesta para cada una de las preguntas
            foreach (var item in respuestasArray)
            {
                //Obtener cada pregunta su id
                Question question = await questionRepository.GetQuestionById(i);
                if (question is null) return null;

                //Obtener cada opción de respuesta su id
                Question_Options questionOption = await question_OptionsRepository.GetQuestion_OptionsByDescription(item);
                if (questionOption is null) return null;

                //Crear un nuevo objeto Answer a partir de los objetos user, question y questionOption
                Answer newAnswer = AnswerRequestToAnswer.MapAnswerRequestToAnswer(user, question, questionOption);

                //Guardar la respuesta en la base de datos
                await answerRepository.CreateAnswer(newAnswer);
                i++;
            }

            //Invocar el método GetDestinationHash del servicio DestinationService para obtener el hash asociado a la combinación de respuestas
            var hash = destinationService.GetDestinationHash(respuestas);

            //Invocar el método GetDestinationByCombination del servicio DestinationService para obtener el destino asociado al hash
            Destination destination = await destinationService.GetDestinationByCombination(hash);


            AnswerResponse answerResponse = new AnswerResponse();

            //Si no se encuentra el destino, devolver un destino por defecto
            if(destination is null){
                answerResponse.First_City = "Bora Bora";
                answerResponse.Second_City = "Dubái";

                return answerResponse;
            }

            //Obtener los nombres de las ciudades de la tabla city asociadas al destino
            answerResponse.First_City = cityService.GetCityById(destination.First_City_Id).Result.NombreDestino;
            answerResponse.Second_City = cityService.GetCityById(destination.Second_City_Id).Result.NombreDestino;

            Console.WriteLine(answerResponse.First_City);
            Console.WriteLine(answerResponse.Second_City);


            //retornar el objeto AnswerResponse con los nombres de las ciudades
            return answerResponse;
        }

        //Método para contar las opciones seleccionadas por los usuarios
        public async Task<List<QuestionOptionCount>> GetQuestionOptionCounts()
        {
            return await answerRepository.GetQuestionOptionCounts();
        }

        //Método para contar las opciones seleccionadas por los usuarios por id de pregunta
        public async Task<List<QuestionOptionCount>> GetQuestionOptionCountsById(int questionId)
        {
            return await answerRepository.GetQuestionOptionCountsById(questionId);
        }

        //Método para obtener las respuestas de un usuario para una pregunta específica
        public async Task<List<Answer>> GetAnswersByUserIdQuestionId(int userId, int questionId)
        {
            return await answerRepository.GetAnswersByUserIdQuestionId(userId, questionId);
        }

        //Método para obtener las respuestas de un usuario
        public async Task<List<Answer>> GetAnswersByUserId(int userId)
        {
            return await answerRepository.GetAnswersByUserId(userId);
        }

        //Método para crear una respuesta de una sola pregunta
        //Recibe un objeto AnswerRequest y devuelve un objeto Answer
        public async Task<Answer?> CreateAnswer(AnswerRequest answerRequest)
        {
            //Validar y obtener el objeto User asociado a la respuesta
            var user = await userRepository.GetUserById(answerRequest.UserId);

            //Validar y obtener el objeto Question asociado a la respuesta
            var question = await questionRepository.GetQuestionById(answerRequest.QuestionId);

            //Validar y obtener el objeto Question_Options asociado a la respuesta
            var questionOption = await question_OptionsRepository.GetQuestion_OptionsById(answerRequest.QuestionOptionId);

            //Crear un nuevo objeto Answer a partir de los objetos user, question y questionOption
            Answer newAnswer = AnswerRequestToAnswer.MapAnswerRequestToAnswer(user, question, questionOption);

            //Obtener la respuesta anterior del usuario para la pregunta
            var pastAnswer = await answerRepository.GetAnswersByUserIdQuestionId(answerRequest.UserId, answerRequest.QuestionId);

            //Si no hay respuesta anterior, crear una nueva respuesta
            if(pastAnswer.Count == 0)
            {
                var createdAnswer = await answerRepository.CreateAnswer(newAnswer);
                return createdAnswer;
            }
            return null;

        }

    }
}