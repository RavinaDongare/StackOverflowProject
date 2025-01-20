using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflowProject.ViewModels
{
  public  class VoteViewModel
    {
        [Required]
        public int VoteID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int AnswerID { get; set; }
        [Required]
        public int VoteValue { get; set; }
    }
}
