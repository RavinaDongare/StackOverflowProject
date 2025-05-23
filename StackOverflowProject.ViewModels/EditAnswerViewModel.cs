﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace StackOverflowProject.ViewModels
{
   public class EditAnswerViewModel
    {[Required]
        public int AnswerID { get; set; }
        [Required]
        public string AnswerTest { get; set; }
        [Required]
        public DateTime AnswerDateTime { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int QuestionID { get; set; }
        [Required]
        public int VotesCount { get; set; }


        public virtual QuestionViewModel Question { get; set; }
       

    }
}
