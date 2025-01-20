using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using StackOverflowProject.ServiceLayer;
using StackOverflowProject.ViewModels;

namespace StackOverflowProject.ApiControllers
{
    public class QuestionController : ApiController
    {
       //IQuestionService questionService;
        IAnswerService answerService;

        public QuestionController( AnswerService ans)
        {
            //this.questionService = qs;
            this.answerService = ans;
        }
       public void Post(int AnswerID, int UserID, int value)
        {
            this.answerService.UpdateAnswerVotesCount(AnswerID, UserID, value);

        }
    }
}