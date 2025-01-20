using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverflowProject.CustomFilter;
using StackOverflowProject.ServiceLayer;
using StackOverflowProject.ViewModels;

namespace StackOverflowProject.Controllers
{
    public class QuestionsController : Controller
    {
        IQuestionService questionService;
        ICategoryService categoryService;
        IAnswerService answerService;

        public QuestionsController(QuestionService qs, CategoryService cs,AnswerService ans)
        {
            this.questionService = qs;
            this.categoryService = cs;
            this.answerService = ans;

        }
        public ActionResult View(int id)
        {
            this.questionService.UpdateQuestionViewsCount(id, 1);
            int uid = Convert.ToInt32(Session["CurrentUserId"]);
           QuestionViewModel questionViewModel= this.questionService.GetQuestionByQuestionId(id, uid);
            return View(questionViewModel);
        }

        //public ActionResult Index()
        //{
        //    return View();
        //}
                
        [ValidateAntiForgeryToken]
        [HttpPost]
        [UserAuthorizationFilterAttribute]

        public ActionResult AddAnswer(NewAnswerViewModel Nanvm)
        {
            Nanvm.UserID = Convert.ToInt32(Session["CurrentUserId"]);
            Nanvm.AnswerDateTime = DateTime.Now;
            Nanvm.VotesCount = 0;

            if (ModelState.IsValid)
            {
                this.answerService.InsertAnswer(Nanvm);
                return RedirectToAction("View", "Questions", new { id= Nanvm.QuestionID});
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                QuestionViewModel questionViewModel = this.questionService.GetQuestionByQuestionId(Nanvm.QuestionID, Nanvm.UserID);
                return View(questionViewModel);
            }

        }

      
        [ValidateAntiForgeryToken]
        [HttpPost]
        [UserAuthorizationFilterAttribute]
        public ActionResult EditAnswers(EditAnswerViewModel Eanvm)
        {
            if (ModelState.IsValid)
            {
                Eanvm.UserID = Convert.ToInt32(Session["CurrentUserId"]);
                this.answerService.UpdateAnswer(Eanvm);
                return RedirectToAction("View", new { id = Eanvm.QuestionID });
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                return RedirectToAction("View", new { id = Eanvm.QuestionID });
            }
        }

        
        public ActionResult Create()
        {
            List<CategoryViewModel> categoryViewModels = this.categoryService.GetCategories();
            ViewBag.category = categoryViewModels;
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [UserAuthorizationFilterAttribute]
        public ActionResult Create(NewQuestionViewModel Qvm)
        {
            if (ModelState.IsValid)
            {
                Qvm.AnswersCount = 0;
                Qvm.ViewsCount = 0;
                Qvm.VotesCount = 0;
                Qvm.QuestionDateAndTime = DateTime.Now;
                Qvm.UserID = Convert.ToInt32(Session["CurrentUserId"]);
                this.questionService.InsertQuestion(Qvm);
                return RedirectToAction("Questions", "Home");


            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                 return View();
            }
        }


    }
}