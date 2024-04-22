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
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public IActionResult Login([FromForm] string username, [FromForm] string password, [FromForm] bool rememberMe)
        {
            User user = _context.User.FirstOrDefault(a => a.Username == username && a.Password == password);
            return View(user);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            bool usernameRepeated = _context.User.Count(a => a.Username == model.Username) > 0;
            bool emailIsUsed = _context.User.Count(a => a.Email == model.Email) > 0;
            bool passwordError = model.Password != model.PasswordAgain;

            if (usernameRepeated || emailIsUsed || passwordError)
            {
                TempData["usernameRepeated"] = usernameRepeated ? "User name repeated." : "";
                TempData["emailIsUsed"] = emailIsUsed ? "This email is used." : "";
                TempData["passwordError"] = passwordError ? "Password is not same as first input." : "";
                return View(model);
			}

            return RedirectToAction("RegisterSucceed");
        }

        [HttpGet]
        public IActionResult CheckEmail(string email)
        {
            return Json(_context.User.Count(a => a.Email == email) > 0);
        }

		[HttpGet]
		public IActionResult CheckUsername(string username)
		{
			return Json(_context.User.Count(a => a.Username == username) > 0);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
