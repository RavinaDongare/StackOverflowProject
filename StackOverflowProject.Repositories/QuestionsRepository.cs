using StackOverflowProject.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowProject.Repositories
{

    public interface IQuestionsRepository
    {

        void InsertQuestion(Question question);
        void UpdateQuestionDetails(Question question);
        void UpdateQuestionVotesCount(int qid, int value);
        void UpdateQuestionAnswersCount(int qid, int value);
        void UpdateQuestionViewsCount(int qid, int value);
        void DeleteQuestion(int Qid);
        List<Question> GetQuestions();
        List<Question> GetQuestionByQuestionId(int Qid);

    }
        public  class QuestionsRepository:IQuestionsRepository
    {
        StackOverflowDbContext dbContext;
        public QuestionsRepository()
        {
            dbContext = new StackOverflowDbContext();
        }

        public void InsertQuestion(Question question)
        {
            dbContext.Questions.Add(question);
            dbContext.SaveChanges();

        }
        public void UpdateQuestionDetails(Question question)
        {
            Question questions = dbContext.Questions.Where(temp => temp.QuestionId == question.QuestionId).FirstOrDefault();
            questions.QuestionName = question.QuestionName;
            questions.QuestionDateAndTime = question.QuestionDateAndTime;
            questions.CategoryID = question.CategoryID;
            dbContext.SaveChanges();
        }
        public void UpdateQuestionVotesCount(int qid, int value)
        {

            Question questions = dbContext.Questions.Where(temp => temp.QuestionId == qid).FirstOrDefault();
            questions.VotesCount = value;
            dbContext.SaveChanges();

        }
        public void UpdateQuestionAnswersCount(int qid, int value)
        {
            Question questions = dbContext.Questions.Where(temp => temp.QuestionId == qid).FirstOrDefault();
            if (questions != null)
            {
                questions.AnswersCount = questions.AnswersCount + value;
                dbContext.SaveChanges();

            }
           

        }
        public void UpdateQuestionViewsCount(int qid, int value)
        {

            Question questions = dbContext.Questions.Where(temp => temp.QuestionId == qid).FirstOrDefault();
            if (questions != null)
            {
                questions.ViewsCount = value;
                dbContext.SaveChanges();

            }
        }
        public void DeleteQuestion(int Qid)
        {
            Question questions = dbContext.Questions.Where(temp => temp.QuestionId == Qid).FirstOrDefault();
            if (questions != null)
            {

                dbContext.Questions.Remove(questions);
                dbContext.SaveChanges();
            }


            }
        public List<Question> GetQuestions()
        {

           List<Question> questions = dbContext.Questions.OrderByDescending(temp => temp.QuestionDateAndTime).ToList();

           List<User> Users = dbContext.Users.ToList();
            return questions;
        }
        public List<Question> GetQuestionByQuestionId(int Qid)
        {

            List<Question> questions = dbContext.Questions.Where(temp => temp.QuestionId == Qid).ToList();

            return questions;
        }

    }
}
