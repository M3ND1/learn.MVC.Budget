using Budget.Data;
using Budget.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Budget.Controllers
{
    public class CategoryController : Controller
    {
        private readonly BudgetContext _context;

        public CategoryController(BudgetContext context)
        {
            _context = context;   
        }
        // GET: CategoryController
        public ActionResult Index()
        {
            var data = _context.Categories.ToList();
            return View(data);
        }
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("Name")] Category category)
        {
            if(ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
            } else return View(category);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            var data = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (data == null)
            {
                return NotFound(); //eo internet
            }
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Name")] Category newcategory)
        { 
            if (!ModelState.IsValid)
                return View(newcategory);

            var exsitingCat = _context.Categories.FirstOrDefault(c => c.Id == id);
            exsitingCat.Name = newcategory.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id) 
        {
            var categoryId = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (categoryId == null)
                return NotFound();

            return View(categoryId);
        }
        public IActionResult Delete(int id)
        {
            var categoryId = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (categoryId == null)
                return NotFound();

            return View(categoryId);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var categoryId = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (categoryId == null)
                return NotFound();
            Debug.WriteLine("DEBUG WRITELINE");
            _context.Categories.Remove(categoryId);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
