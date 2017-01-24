using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FinalExamProgram.DAL;
using FinalExamProgram.Models;

namespace FinalExamProgram.Controllers
{
    public class HomeController : Controller
    {
        private TriviaCrackContext db = new TriviaCrackContext();

        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form, bool rememberMe = false)
        {

            String userName = form["Username"].ToString();
            String password = form["Password"].ToString();

            var currentUser = db.Database.SqlQuery<Users>(
                "SELECT * " +
                "FROM Users " +
                "WHERE UserName = '" + userName + "' AND UserPassword = '" + password + "';");

            if (currentUser.Count() > 0)
            {
                FormsAuthentication.SetAuthCookie(userName, rememberMe);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        // GET: Users/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserID,UserName,UserPassword,UserScore")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(users);
        }


        [Authorize]
        public ActionResult Play()
        {

            IEnumerable<Question> QandA = db.Database.SqlQuery<Question>(
                "SELECT * " +
                "FROM Question " +
                "WHERE Answered = 0; ");

            Users Player = db.Database.SqlQuery<Users>(
                "SELECT * FROM Users WHERE UserName = '" + User.Identity.Name + "';"
                ).FirstOrDefault();

            ViewBag.Player = Player;
            ViewBag.Categories = db.Questions;


            return View(QandA);
        }

        public ActionResult AnswerQuestion(int? QuestionID)
        {
            Question SelectedQuestion = db.Questions.Find(QuestionID);

            ViewBag.Question = SelectedQuestion;


            return View();
        }

        public ActionResult CreateQuestion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateScore(FormCollection form, int? QuestionID)
        {
            String Answer = form["Answer"].ToString();
            int Points = 0;

            var AnsweredCorrect = db.Database.SqlQuery<Question>(
                "SELECT * FROM Question WHERE QuestionID = " + QuestionID + " AND CorrectAnswer = " + Answer + ";"
                );


            db.Database.ExecuteSqlCommand(
                "UPDATE Question SET ANSWERED = 1 WHERE QuestionID = " + QuestionID + ";"
                );

            if (AnsweredCorrect.Count() > 0)
            {
                Points = 50;

                db.Database.ExecuteSqlCommand(
                    "UPDATE Users SET UserScore = (UserScore + " + Points + ") WHERE UserName = '" + User.Identity.Name + "';"
                    );

                return RedirectToAction("Play");
            }
            else
            {
                return RedirectToAction("Play");
            }
            
        }





    }
}