using Anamneseprod.Data;
using Anamneseprod.Models;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Anamneseprod.Controllers
{
    public class CodingController : Controller
    {
        private List<QuestionnaireViewModel> _questions=new List<QuestionnaireViewModel>();
        private readonly EigenanamneseDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _FhirApiUrl;
        private readonly ILogger<CodingController> _logger;
        public CodingController(EigenanamneseDbContext context, IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings, ILogger<CodingController> logger)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _FhirApiUrl = apiSettings.Value.FhirApiurl;
            _logger = logger;
        }
        public IActionResult Index()
        {
            List<string> koerperteile = new List<string> { "Kopf", "Bauch", "Rücken", "Arm/Hand", "Bein/Fuß", "Hals", "Brust", "Ganzer Körper" };
            return View(koerperteile);
            
        }
       

        [HttpPost]
        public IActionResult Groborgan(string selectedItem)
        {
            if (string.IsNullOrEmpty(selectedItem))
            {
                return BadRequest();
            }

            List<Models.Coding> codingList = _context.Codings
                .Where(a => a.Category == selectedItem)
                .ToList();

            return View("ShowDetails", codingList);
        }
        [HttpPost]
        public IActionResult ShowDetails(string selectedDetail)
        {
            if (string.IsNullOrEmpty(selectedDetail))
            {
                return BadRequest();
            }

            List<Symptom> symptomsfromdb = _context.Symptoms.Where(a => a.CodeID == selectedDetail).ToList();

            return View("ShowSymptom", symptomsfromdb);
        }
        [HttpPost]
        public async Task< IActionResult> ShowSymptoms(int id)
        {

            var hauptsymptom = await _context.Symptoms.FindAsync(id);
            if (hauptsymptom == null)
            {
                return NotFound();
            }
            string codeID = hauptsymptom.CodeID;
            List<Question> questionList = await GetQuestionsByCodeId(codeID);
            var viewModel = new QuestionnaireViewModel
            {
                Questions = questionList,
                CurrentPage = 0,
                CodeId = codeID,
                SelectedAnswer = null,
                Answers = new List<QuestionnaireAnswerModel>()
            };
            //_questions.Clear();
            return View("Questionnaire",viewModel) ;
        }
        [HttpGet]
        public async Task<IActionResult> Questionnaire(string codeId, int currentPage = 0)
        {
            try
            {
                var questions = await GetQuestionsByCodeId(codeId);
                var answers = await GetAnswersFromTempData();

                if (currentPage >= questions.Count)
                {
                    return await ProcessCompletedQuestionnaire(codeId, answers);
                }

                var viewModel = new QuestionnaireViewModel
                {
                    Questions = questions,
                    CurrentPage = currentPage,
                    CodeId = codeId,
                    SelectedAnswer = null,
                    Answers = answers
                };

                TempData.Keep("Answers");
                return View(viewModel);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error in Questionnaire method");
                return View("Error", new ErrorViewModel { Message = "Ein unerwarteter Fehler ist aufgetreten." });
            }
        }

        private async Task<List<QuestionnaireAnswerModel>> GetAnswersFromTempData()
        {
            if (TempData.ContainsKey("Answers"))
            {
                string json = TempData["Answers"].ToString();
                return JsonSerializer.Deserialize<List<QuestionnaireAnswerModel>>(json);
            }
            return new List<QuestionnaireAnswerModel>();
        }

        private async Task<IActionResult> ProcessCompletedQuestionnaire(string codeId, List<QuestionnaireAnswerModel> answers)
        {
            var answerIds = answers.Select(q => q.SelectedAnswer).ToList();
            var userChoices = await GetQuestionsByListAnswerId(answerIds);
            var questionnaire = await _context.Questionnaires.FirstOrDefaultAsync(q => q.CodeID == codeId);

            if (questionnaire == null)
            {
                return View("Error", new ErrorViewModel { Message = "Fragebogen nicht gefunden." });
            }

            // TODO: Replace with actual patient ID after implementing identity
            int patientFhir = 765072;

            var newFhirQuestionnaireRes = ConvertToFhirQuestionnaire(userChoices, patientFhir, questionnaire.FhirID);
            var fhirId = await SendToFhirServer(newFhirQuestionnaireRes);

            if (string.IsNullOrEmpty(fhirId))
            {
                return View("Error", new ErrorViewModel { Message = "Fehler beim Senden des Questionnaires an den FHIR-Server." });
            }

            var eigenanamnese = new Eigenanamnese
            {
                AnamneseID = Guid.NewGuid().ToString(),
                FhirID = fhirId,
                PatientID = patientFhir.ToString(),
                QuestionaireID = questionnaire.FhirID,
				description=questionnaire.Category
			};

            _context.Eigenanamnesen.Add(eigenanamnese);
            await _context.SaveChangesAsync();

            return View("Completed");
        }

        
        [HttpPost]
        public IActionResult Questionnaire(QuestionnaireViewModel model)
        {
            List<QuestionnaireAnswerModel> answers = new List<QuestionnaireAnswerModel>();
            if (TempData.ContainsKey("Answers"))
            {
                string json = TempData["Answers"].ToString();
                answers = JsonSerializer.Deserialize<List<QuestionnaireAnswerModel>>(json);
            }
            var newAnswer = new QuestionnaireAnswerModel
            {
                CodeId = model.CodeId,
                CurrentPage = model.CurrentPage,
                SelectedAnswer = model.SelectedAnswer
            };

            int index = answers.FindIndex(q => q.CurrentPage == model.CurrentPage && q.CodeId == model.CodeId);
            if (index != -1)
            {
                answers[index] = newAnswer;
            }
            else
            {
                answers.Add(newAnswer);
            }
            string serializedAnswers = JsonSerializer.Serialize(answers);
            TempData["Answers"] = serializedAnswers;

            model.CurrentPage += 1;
            return RedirectToAction("Questionnaire", new { codeId = model.CodeId, currentPage = model.CurrentPage });
        }
        
        public async Task<List<Question>> GetQuestionsByCodeId(string codeId)
        {
            return await _context.Questions
                .Where(q => q.CodeID == codeId)
                .Include(q => q.Answers)
                .ToListAsync();
        }
        public async Task<List<Quest>> GetQuestionsByListAnswerId(List<string> answerIds)
        {
            return await _context.Answers
                .Where(a => answerIds.Contains(a.AnswerID))
                .Select(a => new Quest
                {
                    Title = a.Question.Title,
                    AnswerValue = a.Value
                })
                .ToListAsync();
        }
        private QuestionnaireResponse ConvertToFhirQuestionnaire(List<Quest> quests, int patientId, string questionnaireId)
        {
            var response = new QuestionnaireResponse
            {
                Status = QuestionnaireResponse.QuestionnaireResponseStatus.Completed,  
                Subject = new ResourceReference($"Patient/{patientId}"),
                Questionnaire = $"Questionnaire/{questionnaireId}",
                //Authored = DateTime.UtcNow+"",
                Item = new List<QuestionnaireResponse.ItemComponent>()
            };

            foreach (var quest in quests)
            {
                var item = new QuestionnaireResponse.ItemComponent
                {
                    //LinkId = quest.Title,  
                    Text = quest.Title,
                    Answer = new List<QuestionnaireResponse.AnswerComponent>
                {
                    new QuestionnaireResponse.AnswerComponent
                    {
                        Value = new FhirString(quest.AnswerValue)
                    }
                }
                };

                response.Item.Add(item);
            }

            return response;
        }
        private async Task<string> SendToFhirServer(Hl7.Fhir.Model.QuestionnaireResponse questionnaire)
        {
            var client = _httpClientFactory.CreateClient();
            var json = new FhirJsonSerializer().SerializeToString(questionnaire);
            var content = new StringContent(json, Encoding.UTF8, "application/fhir+json");
            var response = await client.PostAsync($"{_FhirApiUrl}/QuestionnaireResponse", content);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonResponse = JObject.Parse(responseContent);
            return jsonResponse["id"]?.ToString();
        }
        public IActionResult Create()
        {
            //var codingList = GenerateCodingList();
            //var symptomlist = new List<Symptom>
            //{

            //new Symptom { Name = "Knieschmerzen", Description = "Starke Schmerzen im Knie, die bei Bewegung schlimmer werden.",CodeID="SNOMEDCT-72696002" },
            //new Symptom { Name = "Knie steif", Description = "Morgens oder nach längerem Sitzen steif.",CodeID="SNOMEDCT-72696002" },
            //new Symptom { Name = "Knieknacken", Description = "Knacken im Knie bei Bewegung.",CodeID="SNOMEDCT-72696002" },
            //new Symptom { Name = "Schwellung", Description = "Schwellung um das Knie herum, besonders nach Aktivität.", CodeID="SNOMEDCT-72696002" },
            //new Symptom { Name = "Instabilität", Description = "Gefühl der Instabilität oder Wackeligkeit im Knie.", CodeID="SNOMEDCT-72696002" }

            //};
            //_context.Symptoms.AddRange(symptomlist);
            //_context.Codings.AddRange(codingList);
            //_context.SaveChanges();
            return View();
        }
        //public List<Coding> GenerateCodingList(
        //string system = "http://snomed.info/sct",
        //string version = "http://snomed.info/sct/404684003/version/20240701",
        //bool defaultUsersElected = false)
        //{
        //    var codingList = new List<Coding>();

        //    void AddCoding(string code, string display, string category)
        //    {
        //        codingList.Add(new Coding
        //        {
        //            CodeID = $"SNOMEDCT-{code}",
        //            System = system,
        //            Version = version,
        //            Code = code,
        //            Display = display,
        //            UserSelected = defaultUsersElected,
        //            Text = display,
        //            Category = category
        //        });
        //    }

        //    // Kopf
        //    AddCoding("89545001", "Stirn", "Kopf");
        //    AddCoding("81745001", "Augen", "Kopf");
        //    AddCoding("45206002", "Nase", "Kopf");
        //    AddCoding("123851003", "Mund", "Kopf");
        //    AddCoding("117590005", "Ohren", "Kopf");
        //    AddCoding("123703002", "Kinn", "Kopf");

        //    // Hals
        //    AddCoding("45048000", "Nacken", "Hals");
        //    AddCoding("81502006", "Kehlkopf", "Hals");

        //    // Brust
        //    AddCoding("56873002", "Brustbein", "Brust");
        //    AddCoding("113197003", "Rippen", "Brust");
        //    AddCoding("71854001", "Brustmuskulatur", "Brust");

        //    // Bauch
        //    AddCoding("13344001", "Bauchmuskeln", "Bauch");
        //    AddCoding("70258002", "Bauchnabel", "Bauch");
        //    AddCoding("30730003", "Leiste", "Bauch");

        //    // Rücken
        //    AddCoding("421060004", "Wirbelsäule", "Rücken");
        //    AddCoding("79601000", "Schulterblätter", "Rücken");
        //    AddCoding("122496007", "Lendenwirbelsäule", "Rücken");

        //    // Arm/Hand
        //    AddCoding("16982005", "Schulter", "Arm/Hand");
        //    AddCoding("40983000", "Oberarm", "Arm/Hand");
        //    AddCoding("127949000", "Ellbogen", "Arm/Hand");
        //    AddCoding("14975008", "Unterarm", "Arm/Hand");
        //    AddCoding("74670003", "Handgelenk", "Arm/Hand");
        //    AddCoding("7569003", "Finger", "Arm/Hand");

        //    // Bein/Fuß
        //    AddCoding("68367000", "Oberschenkel", "Bein/Fuß");
        //    AddCoding("72696002", "Knie", "Bein/Fuß");
        //    AddCoding("30021000", "Unterschenkel", "Bein/Fuß");
        //    AddCoding("29707007", "Zehen", "Bein/Fuß");

        //    // Ganzkörper
        //    AddCoding("39937001", "Haut", "Ganzkörper");
        //    AddCoding("71616004", "Muskeln", "Ganzkörper");
        //    AddCoding("272673000", "Knochen", "Ganzkörper");
        //    AddCoding("39352004", "Gelenke", "Ganzkörper");
        //    AddCoding("59820001", "Blutgefäße", "Ganzkörper");

        //    return codingList;
        //}
    }
    public class QuestionnaireViewModel
    {
        public List<Question> Questions { get; set; }
        public int CurrentPage { get; set; }
        public string CodeId { get; set; }
        public string SelectedAnswer { get; set; }
        public List<QuestionnaireAnswerModel> Answers { get; set; }
    }

    public class QuestionnaireAnswerModel
    {
        public string CodeId { get; set; }
        public int CurrentPage { get; set; }
        public string SelectedAnswer { get; set; }
    }
    public class Quest
    {
        public string Title { get; set; }
        public string AnswerValue { get; set; }
    }

}
