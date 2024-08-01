using Anamneseprod.Data;
using Anamneseprod.Models;
using Microsoft.AspNetCore.Mvc;

namespace Anamneseprod.Controllers
{
	public class EigenanamneseController : Controller
	{
		private readonly EigenanamneseDbContext _context;
        public EigenanamneseController(EigenanamneseDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
		{
			List<Eigenanamnese> anamnesefromdb= _context.Eigenanamnesen.ToList();
			return View(anamnesefromdb);
		}
	}
}
