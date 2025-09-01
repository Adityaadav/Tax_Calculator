using Microsoft.AspNetCore.Mvc;
using Tax_Calculator.Data;
using Tax_Calculator.Models;
using System.Linq;

namespace Tax_Calculator.Controllers
{
    public class SalaryIncomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalaryIncomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.IncomeFromSalaries.ToList();
            return View(data);
        }

        public IActionResult Details(int id)
        {
            var record = _context.IncomeFromSalaries.Find(id);
            return View(record);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IncomeFromSalary model)
        {
            if (ModelState.IsValid)
            {
                _context.IncomeFromSalaries.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var record = _context.IncomeFromSalaries.Find(id);
            return View(record);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IncomeFromSalary model)
        {
            if (ModelState.IsValid)
            {
                _context.IncomeFromSalaries.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var record = _context.IncomeFromSalaries.Find(id);
            return View(record);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var record = _context.IncomeFromSalaries.Find(id);
            _context.IncomeFromSalaries.Remove(record);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
