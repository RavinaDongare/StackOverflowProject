using StackOverflowProject.CustomFilter;
using StackOverflowProject.ServiceLayer;
using StackOverflowProject.ViewModels;
using System;
using System.Web.Mvc;


namespace StackOverflowProject.Controllers
{
    public class AccountController : Controller
    {
        IUserService userService;
        public AccountController(UserService Us)
        {
            this.userService = Us;
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            LoginViewModel lvm = new LoginViewModel();
            return View(lvm);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                UsersViewModel uvm = userService.GetUsersByEailAndPassword(lvm.Email, lvm.Password);
                if (uvm != null)
                {
                    Session["CurrentUserId"] = uvm.UserID;
                    Session["CurrentUserName"] = uvm.Name;
                    Session["CurrentUserEmail"] = uvm.Email;
                    Session["CurrentUserMobile"] = uvm.Mobile;
                    Session["CurrentUserPassword"] = uvm.Password;
                    Session["CurrentUserIsAdmin"] = uvm.IsAdmin;
                    if (uvm.IsAdmin == true)
                    {
                        return RedirectToRoute(new { area = "admin", Controller = "AdminHome", Action = "Index" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                else
                {
                    ModelState.AddModelError("x", "Invalid Email/Password");
                    return View(lvm);
                }
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");

                return View(lvm);
            }

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {

            if (ModelState.IsValid)
            {
                int uid = this.userService.InsertUser(registerViewModel);
                Session["CurrentUserId"] = uid;
                Session["CurrentUserName"] = registerViewModel.Name;
                Session["CurrentUserEmail"] = registerViewModel.Email;
                Session["CurrentUserMobile"] = registerViewModel.Mobile;
                Session["CurrentUserPassword"] = registerViewModel.Password;
                Session["CurrentUserIsAdmin"] = false;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                return View();
            }


        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        
        [UserAuthorizationFilterAttribute]
        public ActionResult ChangeProfile()
        {
            int uid = Convert.ToInt32(Session["CurrentUserId"]);
            UsersViewModel uvm = this.userService.GetUsersByUserID(uid);
            EditUserDetailsViewModel eudvm = new EditUserDetailsViewModel { Email=uvm.Email, Name = uvm.Name, Mobile = uvm.Mobile, UserID = uvm.UserID };
            return View(eudvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilterAttribute]
        public ActionResult ChangeProfile(EditUserDetailsViewModel eudvm)
        {
            if (ModelState.IsValid)
            {
                eudvm.UserID = Convert.ToInt32(Session["CurrentUserId"]);
                this.userService.UpdateUserDetails(eudvm);
                Session["CurrentUserName"] = eudvm.Name;
              //  Session["CurrentUserEmail"] = eudvm.Email;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                return View(eudvm);
            }
           
        }

        //[UserAuthorizationFilterAttribute]
        public ActionResult ChangePassword()
        {
            int uid = Convert.ToInt32(Session["CurrentUserId"]);
            UsersViewModel uvm = this.userService.GetUsersByUserID(uid);
            EditUserPasswordViewModel eudvm = new EditUserPasswordViewModel { Email = uvm.Email, Password="",ConfirmPassword="", UserID = uvm.UserID };
            return View(eudvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[UserAuthorizationFilterAttribute]
        public ActionResult ChangePassword(EditUserPasswordViewModel eudvm)
        {
            if (ModelState.IsValid)
            {
                eudvm.UserID = Convert.ToInt32(Session["CurrentUserId"]);
                this.userService.UpdateUserPassword(eudvm);
                //  Session["CurrentUserEmail"] = eudvm.Email;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                return View(eudvm);
            }

        }

    }
}