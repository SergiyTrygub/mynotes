using System.Threading.Tasks;
using MyNotes.Web.Services;
using MyNotes.Web.MultiTenancy;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MyNotes.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITenantsService _tenantsService;

        public HomeController(ITenantsService tenantsService)
        {
            _tenantsService = tenantsService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateList()
        {
            var result = await _tenantsService.CreateAsync();
            if (result.Succeeded && result.Item is AppTenant)
            {
                return Redirect("/" + ((AppTenant)result.Item).Id);
            }
            return BadRequest(result.Errors);
        }
    }
}
