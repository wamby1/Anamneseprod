using Anamneseprod.Data;
using Anamneseprod.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Anamneseprod.Controllers
{
    public class QuestionController : Controller
    {
        private readonly EigenanamneseDbContext _context;
        //private static List<Question> _questions = new List<Question>();
        public QuestionController( EigenanamneseDbContext context)
        {
            _context = context;
        }

        public  IActionResult Index()
        {
            //return View(_questions);
            return View();
        }
        
        //[HttpPost]
        //public IActionResult Create(Question question)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        question.QuestionID = Guid.NewGuid().ToString();
        //        _questions.Add(question);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(question);
        //}



        public async Task<IActionResult> Create()
        {
            string jsondata = @"
        {
            ""questions"": [
                {
                    ""text"": ""Wie lange haben Sie schon Schmerzen im Knie?"",
                    ""options"": [
                        ""Weniger als eine Woche"",
                        ""1-2 Wochen"",
                        ""Mehrere Wochen bis Monate"",
                        ""Mehr als ein Jahr""
                    ]
                },
                {
                    ""text"": ""Können Sie den Schmerz genauer beschreiben?"",
                    ""options"": [
                        ""Stechend"",
                        ""Pochend"",
                        ""Dumpf"",
                        ""Wechselnd""
                    ]
                },
                {
                    ""text"": ""Haben Sie in letzter Zeit eine Verletzung am Knie erlitten?"",
                    ""options"": [
                        ""Ja, durch einen Unfall"",
                        ""Ja, durch sportliche Aktivität"",
                        ""Ja, durch eine andere Ursache"",
                        ""Nein""
                    ]
                },
                {
                    ""text"": ""Wann tritt der Schmerz auf?"",
                    ""options"": [
                        ""Konstant"",
                        ""Nur bei Bewegung"",
                        ""Nur bei Belastung"",
                        ""In Ruhe""
                    ]
                },
                {
                    ""text"": ""Wie stark ist Ihr Schmerz auf einer Skala von 1 bis 10, wobei 10 der stärkste Schmerz ist?"",
                    ""options"": [
                        ""1"",
                        ""2"",
                        ""3"",
                        ""4"",
                        ""5"",
                        ""6"",
                        ""7"",
                        ""8"",
                        ""9"",
                        ""10""
                    ]
                },
                {
                    ""text"": ""Nehmen Sie aktuell irgendwelche Medikamente gegen die Schmerzen oder für andere gesundheitliche Probleme?"",
                    ""options"": [
                        ""Ja, gegen die Schmerzen"",
                        ""Ja, für andere gesundheitliche Probleme"",
                        ""Ja, für beides"",
                        ""Nein""
                    ]
                },
                {
                    ""text"": ""Haben Sie irgendwelche anderen Symptome bemerkt?"",
                    ""options"": [
                        ""Schwellungen"",
                        ""Rötungen"",
                        ""Wärmegefühl"",
                        ""Keine anderen Symptome""
                    ]
                },
                {
                    ""text"": ""Beeinflussen die Schmerzen Ihren Alltag?"",
                    ""options"": [
                        ""Ja, ich kann nicht normal gehen"",
                        ""Ja, ich kann nicht lange stehen"",
                        ""Ja, ich kann keinen Sport treiben"",
                        ""Nein, es beeinflusst mich nicht""
                    ]
                },
                {
                    ""text"": ""Haben Sie in der Vergangenheit ähnliche Knieprobleme gehabt?"",
                    ""options"": [
                        ""Ja, und sie wurden operiert"",
                        ""Ja, und sie wurden anders behandelt"",
                        ""Ja, aber sie wurden nicht behandelt"",
                        ""Nein""
                    ]
                },
                {
                    ""text"": ""Gibt es in Ihrer Familie eine Vorgeschichte von Knieproblemen oder anderen Gelenkerkrankungen?"",
                    ""options"": [
                        ""Ja, Knieprobleme"",
                        ""Ja, andere Gelenkerkrankungen"",
                        ""Ja, beides"",
                        ""Nein""
                    ]
                }
            ]
        }
        ";
            string codeID = "SNOMEDCT-72696002";
            await SaveQuestionsAndAnswers(jsondata,codeID);
            return View();
        }
        public async Task SaveQuestionsAndAnswers(string jsonData, string codeid)
        {
            var data = JObject.Parse(jsonData);
            var questions = data["questions"].ToObject<List<JObject>>();

            foreach (var questionObj in questions)
            {
                var question = new Question
                {
                    QuestionID = Guid.NewGuid().ToString(),
                    Title = questionObj["text"].ToString(),
                    Multichoice = true,
                    CodeID = codeid
                };

                var options = questionObj["options"].ToObject<List<string>>();
                question.Answers = new List<Answer>();

                foreach (var option in options)
                {
                    var answer = new Answer
                    {
                        AnswerID = Guid.NewGuid().ToString(),
                        Value = option,
                        QuestionID = question.QuestionID
                    };
                    question.Answers.Add(answer);
                    _context.Answers.Add(answer); // Explizites Hinzufügen der Antwort zum Kontext
                }

                _context.Questions.Add(question);
            }

            await _context.SaveChangesAsync();
        }
    }
}
