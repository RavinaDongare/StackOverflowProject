using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverflowProject.ServiceLayer;
using StackOverflowProject.ViewModels;

namespace StackOverflowProject.Controllers
{
    public class HomeController : Controller
    {
        IQuestionService questionService;
        ICategoryService categoryService;
        public HomeController(QuestionService qs, CategoryService cs)
        {
            this.questionService = qs;
            this.categoryService = cs;

        }
        public ActionResult Index()
        {
            List<QuestionViewModel> questions = questionService.GetQuestions().ToList();
            return View(questions);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }


        public ActionResult Catrgories()
        {
            List<CategoryViewModel> categories = categoryService.GetCategories().ToList();
            return View(categories);

        }
        [Route("allquestions")]
        public ActionResult Questions()
        {
            List<QuestionViewModel> questions = questionService.GetQuestions().ToList();
            return View(questions);

        }

        public ActionResult Search(string str)
        {
            ViewBag.str = null;
            List<QuestionViewModel> questions = this.questionService.GetQuestions().Where(temp => temp.QuestionName.ToLower().Contains(str.ToLower()) || temp.Category.CategoryName.ToLower().Contains(str.ToLower())).ToList();
            ViewBag.str = str;
              return View(questions);

        }
    }
}