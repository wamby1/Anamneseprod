using Anamneseprod.Classes;
using Anamneseprod.Data;
using Anamneseprod.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Anamneseprod.Controllers
{
    [Authorize(Roles = $"{MyRole.Role_Admin}, {MyRole.Role_User}")]
    public class EigenanamneseController : Controller
	{
		private readonly EigenanamneseDbContext _context;
		
		public EigenanamneseController(EigenanamneseDbContext context)
        {
            _context = context;

		}
		public IActionResult Index()
		{
			var myuser = _context.applicationUsers.Where(u => u.UserName == User.Identity.Name).ToList();
			string patientFhir = myuser[0].FhirID;
			if(string.IsNullOrEmpty(patientFhir) ) { 

				return View();	
			}
			List<Eigenanamnese> anamnesefromdb= _context.Eigenanamnesen.Where(x => x.PatientID==patientFhir).ToList();
			return View(anamnesefromdb);
		}
	}
}
