using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalExamProgram.Models
{

    public class QuestionCategory
    {
        [Key]
        public int QuestionID { get; set; }


        public string CategoryID { get; set; }

        [DisplayName("Question")]
        public string QuestionText { get; set; }

        public string Answer1 { get; set; }

        public string Answer2 { get; set; }

        public string Answer3 { get; set; }

        public string Answer4 { get; set; }

        public int CorrectAnswer { get; set; }

        public bool Answered { get; set; }

        public string CategoryDesc { get; set; }
    }
}