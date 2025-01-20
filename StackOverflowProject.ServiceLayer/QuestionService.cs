using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModels;
using StackOverflowProject.ViewModels;
using StackOverflowProject.Repositories;
using AutoMapper;
using AutoMapper.Configuration;

namespace StackOverflowProject.ServiceLayer
{
    public interface IQuestionService
    {
        void InsertQuestion(NewQuestionViewModel QVM);
        void UpdateQuestionDetails(EditQuestionViewModel QVM);
        void UpdateQuestionVotesCount(int qid, int value);
        void UpdateQuestionAnswersCount(int qid, int value);
        void UpdateQuestionViewsCount(int qid, int value);
        void DeleteQuestion(int Qid);
        List<QuestionViewModel> GetQuestions();
       QuestionViewModel GetQuestionByQuestionId(int Qid, int UserID);
    }
  public   class QuestionService: IQuestionService
    {
        QuestionsRepository questionsRepository;
        public QuestionService()
        {
            questionsRepository = new QuestionsRepository();
        }

        public void InsertQuestion(NewQuestionViewModel QVM)
        {
            var config = new MapperConfiguration(cgf => { cgf.CreateMap<NewQuestionViewModel, Question>(); cgf.IgnoreUnmmaped(); });
            IMapper mapper = config.CreateMapper();
            Question q = mapper.Map<NewQuestionViewModel, Question>(QVM);
            questionsRepository.InsertQuestion(q);
        }
        public void UpdateQuestionDetails(EditQuestionViewModel QVM)
        {
            var config = new MapperConfiguration(cgf => { cgf.CreateMap<EditQuestionViewModel, Question>(); cgf.IgnoreUnmmaped(); });
            IMapper mapper = config.CreateMapper();
            Question q = mapper.Map<EditQuestionViewModel, Question>(QVM);
            questionsRepository.UpdateQuestionDetails(q);

        }
        public  void UpdateQuestionVotesCount(int qid, int value)
        {
            questionsRepository.UpdateQuestionVotesCount(qid, value);
        }
        public void UpdateQuestionAnswersCount(int qid, int value)
        {
            questionsRepository.UpdateQuestionAnswersCount(qid,value);
           
        }
        public void UpdateQuestionViewsCount(int qid, int value)
        {
            questionsRepository.UpdateQuestionViewsCount(qid, value);
        }
        public void DeleteQuestion(int Qid)
        {
            questionsRepository.DeleteQuestion(Qid);
        }
        public List<QuestionViewModel> GetQuestions()
        {
            List<Question> questionViewModels = questionsRepository.GetQuestions().ToList();
            // var config = new MapperConfiguration(cgf => { cgf.CreateMap<Question, QuestionViewModel>(); cgf.IgnoreUnmmaped(); });
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Question, QuestionViewModel>();
                cfg.CreateMap<User, UsersViewModel>();
                cfg.CreateMap<Category, CategoryViewModel>();
                cfg.CreateMap<Answer, AnswerViewModel>();
                cfg.CreateMap<Vote, VoteViewModel>();
                cfg.IgnoreUnmmaped();
            });
            IMapper mapper = config.CreateMapper();
           List<QuestionViewModel> q = mapper.Map<List<Question>, List<QuestionViewModel>>(questionViewModels);
            return q;
        }
        public QuestionViewModel GetQuestionByQuestionId(int Qid,int UserID)
        {
          Question question = questionsRepository.GetQuestionByQuestionId(Qid).FirstOrDefault();
            QuestionViewModel questionViewModels = null;
            if(question != null)
            {
              //  var config = new MapperConfiguration(cgf => { cgf.CreateMap<Question, QuestionViewModel>(); cgf.IgnoreUnmmaped(); });
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Question, QuestionViewModel>();
                    cfg.CreateMap<User, UsersViewModel>();
                    cfg.CreateMap<Category, CategoryViewModel>();
                    cfg.CreateMap<Answer, AnswerViewModel>();
                    cfg.CreateMap<Vote, VoteViewModel>();
                    cfg.IgnoreUnmmaped();
                });
                IMapper mapper = config.CreateMapper();
                questionViewModels = mapper.Map<Question, QuestionViewModel>(question);
            }

            foreach (var item in questionViewModels.Answers)
            {
                item.CurrentUserVoteType = 0;
                VoteViewModel vote = item.Votes.Where(temp => temp.UserID == UserID).FirstOrDefault();
                if (vote != null)
                {
                    item.CurrentUserVoteType = vote.VoteValue;
                }

            }
            return questionViewModels;
        }


    }
}
