using StackOverflowProject.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowProject.Repositories
{

    public interface IAnswersRepository
    {
        void InsertAnswer(Answer Answer);
        void UpdateAnswer(Answer Answer);
        void UpdateAnswerVotesCount(int aid, int uid, int value);
        //  void UpdateAnswerAnswersCount(int aid, int value);
        //   void UpdateAnswerViewsCount(int aid, int value);
        void DeleteAnswer(int Aid);
        List<Answer> GetAnswerByQuestionID(int Qid);
        List<Answer> GetAnswerByAnswerId(int Aid);
    }
    public  class AnswersRepository:IAnswersRepository
    {
        StackOverflowDbContext dbContext;
        IQuestionsRepository qrepository;
        IVotesRepository vrepository;

        public AnswersRepository()
        {
            dbContext = new StackOverflowDbContext();
            IQuestionsRepository questionsRepository = new QuestionsRepository();
            qrepository = new QuestionsRepository();
            vrepository = new VotesRepository();

        }

        public void InsertAnswer(Answer Answer)
        {
            dbContext.Answers.Add(Answer);
            dbContext.SaveChanges();
            IQuestionsRepository questionsRepository = new QuestionsRepository();
            questionsRepository.UpdateQuestionAnswersCount(Answer.QuestionID, 1);

        }

        public void UpdateAnswer(Answer Answer)
        {
            Answer ans = dbContext.Answers.Where(temp => temp.AnswerID == Answer.AnswerID).FirstOrDefault();
            if (ans != null) {
                ans.AnswerTest = Answer.AnswerTest;
                
                dbContext.SaveChanges();
            }
           

        }

       public void UpdateAnswerVotesCount(int aid, int uid, int value)
        {
            Answer ans = dbContext.Answers.Where(temp => temp.AnswerID == aid).FirstOrDefault();
            if (ans != null)
            {
                ans.VotesCount = value;
                dbContext.SaveChanges();

                qrepository.UpdateQuestionVotesCount(ans.QuestionID,value);
                vrepository.UpdateVote(aid,uid,value);
            }

            }

       public void DeleteAnswer(int Aid)
        {

            Answer answer = dbContext.Answers.Where(temp => temp.AnswerID == Aid).FirstOrDefault();
            if (answer != null)
            {

                dbContext.Answers.Remove(answer);
                dbContext.SaveChanges();
                qrepository.UpdateQuestionVotesCount(answer.QuestionID,-1);
            }

        }

       public List<Answer> GetAnswerByQuestionID(int Qid)
        {
            List<Answer> answer = dbContext.Answers.Where(temp => temp.QuestionID == Qid).OrderByDescending(temp=> temp.AnswerDateTime).ToList();
            return answer;
        }

        public List<Answer> GetAnswerByAnswerId(int Aid)
        {
            List<Answer> answer = dbContext.Answers.Where(temp => temp.AnswerID == Aid).ToList();
            return answer;

        }
    }
}
