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
   public interface IAnswerService
    {
        void InsertAnswer(NewAnswerViewModel Answer);
        void UpdateAnswer(EditAnswerViewModel Answer);
        void UpdateAnswerVotesCount(int aid, int uid, int value);
        //  void UpdateAnswerAnswersCount(int aid, int value);
        //   void UpdateAnswerViewsCount(int aid, int value);
        void DeleteAnswer(int Aid);
        List<AnswerViewModel> GetAnswerByQuestionID(int Qid);
        AnswerViewModel GetAnswerByAnswerId(int Aid);
    }
    public class AnswerService:IAnswerService
    {
        AnswersRepository answersRepository;
        public AnswerService()
        {
            answersRepository = new AnswersRepository();
        }

        public void InsertAnswer(NewAnswerViewModel Answer)
        {
            var config = new MapperConfiguration(cgf => { cgf.CreateMap<NewAnswerViewModel, Answer>(); cgf.IgnoreUnmmaped(); });
            IMapper mapper = config.CreateMapper();
            Answer q = mapper.Map<NewAnswerViewModel, Answer>(Answer);
            answersRepository.InsertAnswer(q);

        }
        public void UpdateAnswer(EditAnswerViewModel Answer)
        {
            var config = new MapperConfiguration(cgf => { cgf.CreateMap<EditAnswerViewModel, Answer>(); cgf.IgnoreUnmmaped(); });
            IMapper mapper = config.CreateMapper();
            Answer q = mapper.Map<EditAnswerViewModel, Answer>(Answer);
            answersRepository.UpdateAnswer(q);

        }
        public void UpdateAnswerVotesCount(int aid, int uid, int value)
        {
            answersRepository.UpdateAnswerVotesCount(aid,uid,value);
        }
        
        public void DeleteAnswer(int Aid)
        {
            answersRepository.DeleteAnswer(Aid);
        }
        public List<AnswerViewModel> GetAnswerByQuestionID(int Qid)
        {
            List<Answer> answers = answersRepository.GetAnswerByQuestionID(Qid);
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Answer, AnswerViewModel>();
                cfg.CreateMap<User, UsersViewModel>();
                cfg.CreateMap<Category, CategoryViewModel>();
                cfg.CreateMap<Question, QuestionViewModel>();
                cfg.CreateMap<Vote, VoteViewModel>();
                cfg.IgnoreUnmmaped();
            });
            IMapper mapper = config.CreateMapper();
            List<AnswerViewModel> answerViewModel = mapper.Map<List<Answer>, List<AnswerViewModel>>(answers);
            return answerViewModel;
        }


    
    public AnswerViewModel GetAnswerByAnswerId(int Aid)
        {

            Answer answers = answersRepository.GetAnswerByAnswerId(Aid).FirstOrDefault();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Answer, AnswerViewModel>();
                cfg.CreateMap<User, UsersViewModel>();
                cfg.CreateMap<Category, CategoryViewModel>();
                cfg.CreateMap<Question, QuestionViewModel>();
                cfg.CreateMap<Vote, VoteViewModel>();
                cfg.IgnoreUnmmaped();
            });
            IMapper mapper = config.CreateMapper();
            AnswerViewModel answerViewModel = mapper.Map<Answer, AnswerViewModel>(answers);
            return answerViewModel;

        }
    }
}
