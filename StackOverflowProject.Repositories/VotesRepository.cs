using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{

    public interface IVotesRepository
    {
        void UpdateVote(int aid, int uid,int  value);
    }
    public class VotesRepository : IVotesRepository
    {
        StackOverflowDbContext dbContext;
        public  VotesRepository()
        {
            dbContext = new StackOverflowDbContext();

        }
        public void UpdateVote(int aid, int uid, int value)
        {
            int updateValue;
            if (value > 0) updateValue = 1;
            else if (value < 0) updateValue = -1;
            else updateValue = 0;
            Vote vote = dbContext.Votes.Where(temp => temp.AnswerID == aid).FirstOrDefault();
            if(vote != null)
            {
                vote.VoteValue = updateValue;
            }
            else
            {
                Vote newvotes = new Vote()
                {
                    AnswerID = aid,
                    UserID = uid,
                    VoteValue = updateValue
                };
                dbContext.Votes.Add(newvotes);
                dbContext.SaveChanges();
            }
        }
    }
}
