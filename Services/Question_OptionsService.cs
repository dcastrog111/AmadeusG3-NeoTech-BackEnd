using AmadeusG3_Neo_Tech_BackEnd.Models;
using AmadeusG3_Neo_Tech_BackEnd.Data;
using Microsoft.EntityFrameworkCore;
using AmadeusG3_Neo_Tech_BackEnd.Repositories;
using System.Collections.Generic;
using AmadeusG3_Neo_Tech_BackEnd.Dtos;
using AmadeusG3_Neo_Tech_BackEnd.Mappers;

namespace AmadeusG3_Neo_Tech_BackEnd.Services{

    public class Question_OptionsService{

        
        private readonly Question_OptionsRepository question_optionsRepository;
        private readonly QuestionRepository questionRepository;

        public Question_OptionsService(ApplicationDbContext dbContext){
            
            question_optionsRepository = new Question_OptionsRepository(dbContext);
            questionRepository = new QuestionRepository(dbContext);
        }

        public async Task<List<Question_Options>> GetAllQuestion_Options()
        {
            return await question_optionsRepository.GetAllQuestion_Options();
        }

        public async Task<Question_Options?> GetQuestion_OptionsById(int id)
        {
            return await question_optionsRepository.GetQuestion_OptionsById(id);
        }

        public async Task<Question_Options> CreateQuestion_Options(Question_OptionsRequest question_OptionsRequest)
        {
            var question = await questionRepository.GetQuestionById(question_OptionsRequest.QuestionId);

            Question_Options question_options = Question_OptionsRequestToQuestion_Options.MapQuestion_OptionsRequestToQuestion_Options(question_OptionsRequest, question);

            var newQuestion_options = await question_optionsRepository.CreateQuestion_Options(question_options);
            
            if(newQuestion_options == null){
                return await question_optionsRepository.CreateQuestion_Options(question_options);
            }
            else{
                return newQuestion_options;
            }
        }

        public async Task<List<Question_Options>> GetOptionsByQuestionId(int questionId)
        {
            return await question_optionsRepository.GetOptionsByQuestionId(questionId);
        }

    }
}