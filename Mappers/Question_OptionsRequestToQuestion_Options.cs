using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Dtos;

namespace AmadeusG3_Neo_Tech_BackEnd.Mappers{

    public class Question_OptionsRequestToQuestion_Options{

        public static Question_Options MapQuestion_OptionsRequestToQuestion_Options(Question_OptionsRequest question_OptionsRequest, Question question){
            
            return new Question_Options{
                
                Id = question_OptionsRequest.Id,
                Description = question_OptionsRequest.Description,
                Dato = question_OptionsRequest.Dato,
                UrlImg = question_OptionsRequest.UrlImg,
                Question = question
                
            };
        }
    }
}