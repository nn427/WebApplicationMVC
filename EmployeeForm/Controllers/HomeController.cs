using System.Diagnostics;
using DBLibrary.Models;
using EmployeeForm.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeForm.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly LocalDBContext _dbContext;

		public HomeController(ILogger<HomeController> logger, LocalDBContext dbContext)
		{
			_logger = logger;
			_dbContext = dbContext;
		}

		public IActionResult Index()
		{
			List<Employee> list = _dbContext.Employee.OrderByDescending(a => a.Id).ToList();
			return View(list);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Employee employee)
		{
			employee.EmpId = Guid.NewGuid().ToString();
			_dbContext.Employee.Add(employee);
			_dbContext.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Delete(string empId)
		{
			var employee = _dbContext.Employee.FirstOrDefault(a => a.EmpId == empId);
			_dbContext.Employee.Remove(employee);
			_dbContext.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Edit(string empId)
		{
			var employee = _dbContext.Employee.FirstOrDefault(a => a.EmpId == empId);
			return View(employee);
		}

		[HttpPost]
		public IActionResult Edit(Employee employee)
		{
			string empId = employee.EmpId;
			var newEmp = _dbContext.Employee.FirstOrDefault(a => a.EmpId == empId);
			newEmp.EmpId = employee.EmpId;
			newEmp.Name = employee.Name;
			newEmp.Salary = employee.Salary;
			_dbContext.SaveChanges();

			return RedirectToAction("Index");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}