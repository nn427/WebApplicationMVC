using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UploadFile.Models;

namespace UploadFile.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		private string _imagePath;

		public HomeController(IWebHostEnvironment hostEnv, ILogger<HomeController> logger)
		{
			_logger = logger;
			_imagePath = $@"{hostEnv.WebRootPath}\images";
		}

		private FileInfo[] GetImages()
		{
			DirectoryInfo dInfo = new DirectoryInfo(_imagePath);
			FileInfo[] files = dInfo.GetFiles();
			return files;
		}

		public IActionResult Index()
		{
			return View(GetImages());
		}

		[HttpPost]
		public async Task<IActionResult> Index(IFormFile formFile)
		{
			if (formFile != null && formFile.Length > 0)
			{
				string savePath = $@"{_imagePath}\{formFile.FileName}";
				using (var stream = new FileStream(savePath, FileMode.Create))
				{
					await formFile.CopyToAsync(stream);
				}
			}
			return RedirectToAction("Index");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
