using StackOverflowProject.DomainModels;
using System.Collections.Generic;
using System.Linq;

namespace StackOverflowProject.Repositories
{
    public interface IUsersRepository
    {
        void InsertUser(User u);
        void UpdateUserDetails(User u);
        void UpdateUserPassword(User u);
        void DeleteUser(int Uid);

        List<User> GetUsers();

        List<User> GetUsersByEailAndPassword(string Email, string Password);
        List<User> GetUsersByEmail(string Email);
        List<User> GetUsersByUserID(int UserID);

        int GetLatestUserID();

    }
    public class UsersRepository : IUsersRepository
    {
        StackOverflowDbContext dbContext;
        public UsersRepository()
        {
            dbContext= new StackOverflowDbContext();
        }

        public void InsertUser(User u)
        {
            dbContext.Users.Add(u);
            dbContext.SaveChanges();
        }
       public void UpdateUserDetails(User u)
        {
            User user = dbContext.Users.Where(temp => temp.UserID == u.UserID).FirstOrDefault();
            if (user != null)
            {

                user.Mobile = u.Mobile;
                user.Name = u.Name;

                //  user.Email = u.Email;   as per tha requirment email id should not update
                dbContext.SaveChanges();
            }

        }

       public void UpdateUserPassword(User u)
        {
            User user = dbContext.Users.Where(temp => temp.UserID == u.UserID).FirstOrDefault();
            user.PasswordHash = u.PasswordHash;
            dbContext.SaveChanges();
        }

       public List<User> GetUsers()
        {
            List<User> usersList = dbContext.Users.Where(temp => temp.IsAdmin == false).OrderBy(temp=>temp.Name).ToList();
            return usersList;
        }

        public List<User> GetUsersByEailAndPassword(string Email, string Password)
        {
            List<User> usersList = dbContext.Users.Where(temp => temp.Email == Email && temp.PasswordHash == Password).ToList();
            return usersList;
        }

       public List<User> GetUsersByEmail(string Email)
        {
            List<User> usersList = dbContext.Users.Where(temp => temp.Email == Email).ToList();
            return usersList;
        }

      public  List<User> GetUsersByUserID(int UserID)
        {
            List<User> usersList = dbContext.Users.Where(temp => temp.UserID == UserID).ToList();
            return usersList;
        }

        public int GetLatestUserID()
        {
            int Uid = dbContext.Users.Select(temp => temp.UserID).Max();
            return Uid;
             }

        public void DeleteUser(int Uid)
        {
            User user = dbContext.Users.Where(temp => temp.UserID == Uid).FirstOrDefault();
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
        }

    }
}
