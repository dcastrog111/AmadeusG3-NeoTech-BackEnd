using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Dtos;

namespace AmadeusG3_Neo_Tech_BackEnd.Mappers{

    public class Question_OptionsToQuestion_OptionsResponse{

        public static Question_OptionsResponse MapQuestion_OptionsToQuestion_OptionsResponse(Question_Options question_Options, Question question){
            
            return new Question_OptionsResponse{
                
                Id = question_Options.Id,
                Description = question_Options.Description,
                Dato = question_Options.Dato,
                UrlImg = question_Options.UrlImg,
                QuestionId = question.Id
                
            };
        }
    }
}