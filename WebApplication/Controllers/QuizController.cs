using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Xml.Serialization;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class QuizController : Controller
    {
        private QuestionModel[] questions;

        public QuizController() 
        {
            questions = LoadQuestions();
            
        }

        private QuestionModel[] LoadQuestions() 
        {
            XmlSerializer formatter = new XmlSerializer(typeof(QuestionModel[]));
            using (FileStream fs = new FileStream("questions.xml", FileMode.OpenOrCreate))
            {
                QuestionModel[] questions = formatter.Deserialize(fs) as QuestionModel[];
                return questions;
            }
        }

        public IActionResult Index()
        {
            return View(questions);
        }
    }
}
