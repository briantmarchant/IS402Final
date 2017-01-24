using FinalExamProgram.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FinalExamProgram.DAL
{
    public class TriviaCrackContext : DbContext
    {
        public TriviaCrackContext() : base("TriviaCrackContext")
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Users> User { get; set; }
        public DbSet<QuestionCategory> QuestionCategories { get; set; }
    }
}