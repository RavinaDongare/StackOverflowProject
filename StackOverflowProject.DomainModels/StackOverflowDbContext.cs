using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace StackOverflowProject.DomainModels
{
    public class StackOverflowDbContext : DbContext  //when object of this class get called then automatically connection string in web.config will call
    {
        public DbSet<Category> Categories{ get; set; }
        public DbSet<Vote> Votes{ get; set; }
        public DbSet<Question> Questions{ get; set; }
        public DbSet<Answer>Answers { get; set; }
        public DbSet<User>Users { get; set; }
    }
}
