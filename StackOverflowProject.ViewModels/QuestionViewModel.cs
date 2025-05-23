﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowProject.ViewModels
{
   public class QuestionViewModel
    {

        [Required]
        public int QuestionId { get; set; }

        [Required]
        public string QuestionName { get; set; }

        [Required]
        public DateTime QuestionDateAndTime { get; set; }
        [Required]
        
    
        public int CategoryID { get; set; }

        [Required]

        public int UserID { get; set; }
        [Required]
     

        public int VotesCount { get; set; }

        [Required]
        public int AnswersCount { get; set; }

        [Required]
        public int ViewsCount { get; set; }


        public UsersViewModel User { get; set; }
        public CategoryViewModel Category { get; set; }

        public  List<AnswerViewModel> Answers { get; set; }




    }
}
