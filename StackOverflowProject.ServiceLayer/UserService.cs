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

    public interface IUserService
    {
        int InsertUser(RegisterViewModel vum);
        void UpdateUserDetails(EditUserDetailsViewModel vum);
        void UpdateUserPassword(EditUserPasswordViewModel vum);
        void DeleteUser(int Uid);
        List<UsersViewModel> GetUsers();

        UsersViewModel GetUsersByEailAndPassword(string Email, string Password);
        UsersViewModel GetUsersByEmail(string Email);
        UsersViewModel GetUsersByUserID(int UserID);

        //int GetLatestUserID();

    }
    public  class UserService:IUserService
    {
        UsersRepository repository;
        public UserService()
        {
            repository = new UsersRepository();
        }

        public int InsertUser(RegisterViewModel vum)
        {
            var config = new MapperConfiguration(cgf => { cgf.CreateMap<RegisterViewModel, User>(); cgf.IgnoreUnmmaped(); });
            IMapper mapper = config.CreateMapper();
            User u = mapper.Map<RegisterViewModel, User>(vum);
            u.PasswordHash = SHA256HashGenerator.GenerateHash(vum.Password);
            repository.InsertUser(u);
            int uid = repository.GetLatestUserID();
            return uid;
        }

      public  void UpdateUserDetails(EditUserDetailsViewModel vum)
        {
            var config = new MapperConfiguration(cgf => { cgf.CreateMap<EditUserDetailsViewModel, User>(); cgf.IgnoreUnmmaped(); });
            IMapper mapper = config.CreateMapper();
            User u = mapper.Map<EditUserDetailsViewModel, User>(vum);
            repository.UpdateUserDetails(u);
        }

        public void UpdateUserPassword(EditUserPasswordViewModel vum)
        {
            var config = new MapperConfiguration(cgf => { cgf.CreateMap<EditUserPasswordViewModel, User>(); cgf.IgnoreUnmmaped(); });
            IMapper mapper = config.CreateMapper();
            User u = mapper.Map<EditUserPasswordViewModel, User>(vum);
            u.PasswordHash = SHA256HashGenerator.GenerateHash(vum.Password);
            repository.UpdateUserPassword(u);

        }
       public void DeleteUser(int Uid)
        {
            repository.DeleteUser(Uid);
        }
        public List<UsersViewModel> GetUsers()
        {
            List<User> users=  repository.GetUsers();

            var config = new MapperConfiguration(cgf => { cgf.CreateMap<User, UsersViewModel>(); cgf.IgnoreUnmmaped(); });
            IMapper mapper = config.CreateMapper();
            List < UsersViewModel> u = mapper.Map<List<User>,List<UsersViewModel>>(users);
            return u;
        }

        public UsersViewModel GetUsersByEailAndPassword(string Email, string Password)
        {
            User user = repository.GetUsersByEailAndPassword(Email, SHA256HashGenerator.GenerateHash(Password)).FirstOrDefault();
            //  user.PasswordHash = SHA256HashGenerator.GenerateHash(Password);
            UsersViewModel vum = null;
            if (user != null)
            {
                var config = new MapperConfiguration(cgf => { cgf.CreateMap<User, UsersViewModel>(); cgf.IgnoreUnmmaped(); });
                IMapper mapper = config.CreateMapper();

                vum = mapper.Map<User, UsersViewModel> (user);
               
            }

            return vum;

        }
        public UsersViewModel GetUsersByEmail(string Email)
        {
            User user = repository.GetUsersByEmail(Email).FirstOrDefault();
            //  user.PasswordHash = SHA256HashGenerator.GenerateHash(Password);
            UsersViewModel vum = null;
            if (user != null)
            {
                var config = new MapperConfiguration(cgf => { cgf.CreateMap<User, UsersViewModel>(); cgf.IgnoreUnmmaped(); });
                IMapper mapper = config.CreateMapper();

                vum = mapper.Map<User, UsersViewModel>(user);

            }

            return vum;
        }
        public UsersViewModel GetUsersByUserID(int UserID)
        {
            User user = repository.GetUsersByUserID(UserID).FirstOrDefault();
            //  user.PasswordHash = SHA256HashGenerator.GenerateHash(Password);
            UsersViewModel vum = null;
            if (user != null)
            {
                var config = new MapperConfiguration(cgf => { cgf.CreateMap<User, UsersViewModel>(); cgf.IgnoreUnmmaped(); });
                IMapper mapper = config.CreateMapper();

                vum = mapper.Map<User, UsersViewModel>(user);

            }

            return vum;

        }

        //public int GetLatestUserID()
        //{
        //    User user = repository.GetUsersByUserID(UserID).FirstOrDefault();
        //    //  user.PasswordHash = SHA256HashGenerator.GenerateHash(Password);
        //    UsersViewModel vum = null;
        //    if (user != null)
        //    {
        //        var config = new MapperConfiguration(cgf => { cgf.CreateMap<User, UsersViewModel>(); cgf.IgnoreUnmmaped(); });
        //        IMapper mapper = config.CreateMapper();

        //        vum = mapper.Map<User, UsersViewModel>(user);

        //    }

        //    return vum;
        //}

    }
}
