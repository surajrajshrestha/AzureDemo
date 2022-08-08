using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Vision.Web.Models;

namespace Vision.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITokenService _serviceToken;

        public HomeController(ILogger<HomeController> logger, ITokenService serviceToken)
        {
            _logger = logger;
            _serviceToken = serviceToken;
        }

        public async Task<IActionResult> Index()
        {
            var view = await _serviceToken.GetTokenAsync();
            IList<string> obj = new List<string>();
            obj.Add(view);
            return View(obj as IEnumerable<string>);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}