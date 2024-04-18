using System.Diagnostics;
using LoginForm.Models;
using Microsoft.AspNetCore.Mvc;
using DBLibrary.Models;

namespace LoginForm.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LocalDBContext _context;

        public HomeController(ILogger<HomeController> logger, LocalDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Forgot_pwd()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] string userName, [FromForm] string password, [FromForm] bool rememberMe)
        {
            User user = _context.User.FirstOrDefault(a => a.UserName == userName && a.Password == password);
            if (user != null)
            {
                
            }
            return View(user);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
