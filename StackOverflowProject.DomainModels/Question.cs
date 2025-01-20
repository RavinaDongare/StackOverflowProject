using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflowProject.DomainModels
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }
        public string QuestionName { get; set; }
        public DateTime QuestionDateAndTime { get; set; }
        public int? UserID { get; set; }
      //  public int UserID { get; set; }
        public int? CategoryID { get; set; }
        public int VotesCount { get; set; }
        public int AnswersCount { get; set; }
        public int ViewsCount { get; set; }


       [ForeignKey("UserID")]
       public virtual User User { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }

        public virtual List<Answer> Answers { get; set; }  // we here created list bcz one question can have multiple ans.

    }
}
