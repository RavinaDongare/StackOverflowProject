using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StackOverflowProject.ServiceLayer;
using StackOverflowProject.ViewModels;

namespace StackOverflowProject.ApiControllers
{
    
    public class AccountController : ApiController
    {

        IUserService userService;
        public AccountController(UserService Us)
        {
            this.userService = Us;
        }

        public string Get(string Email)
        {
            if (userService.GetUsersByEmail(Email) != null)
            {
                return "Found";
            }
            else
            {
                return "NotFound";
            }
        }
    }
}
