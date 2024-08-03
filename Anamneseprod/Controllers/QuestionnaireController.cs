using Anamneseprod.Data;
using Anamneseprod.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Options;
using Anamneseprod.Classes;
using Microsoft.AspNetCore.Authorization;
namespace Anamneseprod.Controllers
{
    [Authorize(Roles = MyRole.Role_Admin)]
    public class QuestionnaireController : Controller
    {
        private readonly EigenanamneseDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _FhirApiUrl;
        public QuestionnaireController(EigenanamneseDbContext context, IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _context = context;
            _httpClientFactory = httpClientFactory; 
            _FhirApiUrl= apiSettings.Value.FhirApiurl;
        }
        public IActionResult Index()
        {
            return View();
        }
       
        public async Task<IActionResult> Create()
        {
            string codeId = "SNOMEDCT-72696002";
            try
            {
                var newFhirQuestionnaire = ConvertToFhirQuestionnaire(codeId);
                var fhirId = await SendToFhirServer(newFhirQuestionnaire);
                if (string.IsNullOrEmpty(fhirId))
                {
                    // return View("Error", new ErrorViewModel { Message = "Fehler beim Senden des Questionnaires an den FHIR-Server." });
                    return View("Error");
                }
                var questionnaire = new Models.Questionnaire
                {
                    QuestionaireID = Guid.NewGuid().ToString(), 
                    FhirID = fhirId,
                    Category = newFhirQuestionnaire.Code.FirstOrDefault()?.Display, 
                    CodeID = codeId,
                    Coding = await _context.Codings.FindAsync(codeId)
                };

                _context.Questionnaires.Add(questionnaire);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch(Exception ex) {
               
               // return View("Error", new ErrorViewModel { Message = $"Fehler beim Erstellen des Questionnaires: {ex.Message}" });
               return View("Error", ex);
            }
           

        }
       
        private Hl7.Fhir.Model.Questionnaire ConvertToFhirQuestionnaire(string codeId)
        {
            var coding = _context.Codings.Find(codeId);
            
            if (coding == null)
            {
                throw new ArgumentException($"Coding mit ID {codeId} nicht gefunden.");
            }

            List<Question> questionsFromDb = _context.Questions
                .Where(q => q.CodeID == codeId)
                .Include(q => q.Answers)
                .ToList();
            var questionnaire = new Hl7.Fhir.Model.Questionnaire
            {
                
                Status = Hl7.Fhir.Model.PublicationStatus.Active,
                //Date = DateTime.UtcNow,
                Title = $"Fragebogen für {coding.Display}",
                Code = new List<Hl7.Fhir.Model.Coding>
        {
            new Hl7.Fhir.Model.Coding
            {
                System = coding.System,
                Code = coding.Code,
                Version = coding.Version,
                Display = coding.Display
            }
        },
                Item = new List<Hl7.Fhir.Model.Questionnaire.ItemComponent>()
            };

            foreach (var dbQuestion in questionsFromDb)
            {
                var questionItem = new Hl7.Fhir.Model.Questionnaire.ItemComponent
                {
                    LinkId = dbQuestion.QuestionID,
                    Text = dbQuestion.Title,
                    Type = Hl7.Fhir.Model.Questionnaire.QuestionnaireItemType.String,
                    Required = true,
                    Repeats = false,
                    AnswerOption = new List<Hl7.Fhir.Model.Questionnaire.AnswerOptionComponent>()
                };

                foreach (var dbAnswer in dbQuestion.Answers)
                {
                    questionItem.AnswerOption.Add(new Hl7.Fhir.Model.Questionnaire.AnswerOptionComponent
                    {
                        Value = new Hl7.Fhir.Model.FhirString(dbAnswer.Value)
                    });
                }

                questionnaire.Item.Add(questionItem);
            }

            return questionnaire;

        }
        private async Task<string> SendToFhirServer(Hl7.Fhir.Model.Questionnaire questionnaire)
        {
            var client = _httpClientFactory.CreateClient();
            var json = new FhirJsonSerializer().SerializeToString(questionnaire);
            var content = new StringContent(json, Encoding.UTF8, "application/fhir+json");
            var response = await client.PostAsync($"{_FhirApiUrl}/Questionnaire", content);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonResponse = JObject.Parse(responseContent);
            return jsonResponse["id"]?.ToString();
        }
    }
}
