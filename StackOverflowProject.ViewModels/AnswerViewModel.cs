using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StackOverflowProject.ViewModels
{
    public class AnswerViewModel
    {
        public int AnswerID { get; set; }
        public string AnswerTest { get; set; }
        public DateTime AnswerDateTime { get; set; }
        public int UserID { get; set; }
        public int QuestionID { get; set; }
        public int VotesCount { get; set; }

     
        public virtual UsersViewModel User { get; set; }
        public virtual List<VoteViewModel> Votes { get; set; }
        public virtual List<QuestionViewModel> Questions { get; set; }//added
        public int CurrentUserVoteType { get; set; }
    }
}
