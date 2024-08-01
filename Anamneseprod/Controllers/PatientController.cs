using Anamneseprod.Data;
using Anamneseprod.Models;
using System.Net.Http;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text;
using Microsoft.Extensions.Options;


namespace Anamneseprod.Controllers
{
    public class PatientController : Controller
    {
        private readonly EigenanamneseDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _FhirApiUrl;
        
        public PatientController(EigenanamneseDbContext context, IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings )
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _FhirApiUrl = apiSettings.Value.FhirApiurl;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Patientdata obj)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}
            try
            {
                var fhirPatient = ConvertToFhirPatient(obj);
                var fhirId = await SendToFhirServer(fhirPatient);
                if (string.IsNullOrEmpty(fhirId))
                {
                    ModelState.AddModelError(string.Empty, "Fehler beim Speichern des FHIR-Patienten.");
                    return View(obj);
                }
                obj.FhirID = fhirId;
                string currentDate = DateTime.Now.ToString("yyyyMMdd");
                string patientId = $"{currentDate}-{fhirId}";
                obj.PatientID=patientId;
                _context.Patients.Add(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
            return View("Index");
        }
        private Hl7.Fhir.Model.Patient ConvertToFhirPatient(Patientdata obj)
        {
            return new Hl7.Fhir.Model.Patient
            {
                Name = new List<HumanName>
            {
                new HumanName
                {
                    Family = obj.NameFamily,
                    Given = new string[] { obj.NameGiven }
                }
            },
                Gender = Enum.Parse<AdministrativeGender>(obj.Gender,true),
                BirthDate = obj.Birthdate,
                Address = new List<Address>
            {
                new Address
                {
                    Line = new string[] { obj.AddressLine },
                    City = obj.AddressCity,
                    PostalCode = obj.AddressPostalCode.ToString()
                }
            },
                Telecom = new List<ContactPoint>
            {
                new ContactPoint
                {
                    System = ContactPoint.ContactPointSystem.Phone,
                    Value = obj.Phone
                },
                new ContactPoint
                {
                    System = ContactPoint.ContactPointSystem.Email,
                    Value = obj.Email,
                    Use = ContactPoint.ContactPointUse.Home
                }
            }
            };
        }

        private async Task<string> SendToFhirServer(Hl7.Fhir.Model.Patient fhirPatient)
        {
            var client = _httpClientFactory.CreateClient();
            var json = new FhirJsonSerializer().SerializeToString(fhirPatient);
            var content = new StringContent(json, Encoding.UTF8, "application/fhir+json");
            var response = await client.PostAsync($"{_FhirApiUrl}/Patient", content);

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
