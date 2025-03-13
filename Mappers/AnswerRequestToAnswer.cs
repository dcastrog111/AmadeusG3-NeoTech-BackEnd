using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Dtos;

namespace AmadeusG3_Neo_Tech_BackEnd.Mappers{

    public class AnswerRequestToAnswer{
        public static Answer MapAnswerRequestToAnswer(User user, Question question, Question_Options question_Option){
            
            return new Answer{

                User = user,
                Question = question,
                RegistDate = DateTime.UtcNow,
                Question_Option = question_Option

            };
        }
    }
}