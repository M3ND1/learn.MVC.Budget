using Budget.Data;
using Budget.Interfaces;
using Budget.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Budget.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        // GET: CategoryController
        public ActionResult Index()
        {
            var data = _categoryRepository.GetAllCategories();
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
                _categoryRepository.AddCategory(category);
                return RedirectToAction(nameof(Index));
            }
            else return View(category); //BadRequest(ModelState)
        }
        public IActionResult Edit(int id)
        {
            var data = _categoryRepository.FindById(id);
            if (data == null)
                return NotFound(); //eo internet

            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Name")] Category newcategory)
        { 
            if (!ModelState.IsValid)
                return View(newcategory);
            else 
                _categoryRepository.EditCategory(id, newcategory);
           
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id) 
        {
            var categoryId = _categoryRepository.FindById(id);
            if (categoryId == null)
                return NotFound();

            return View(categoryId);
        }
        public IActionResult Delete(int id)
        {
            var categoryId = _categoryRepository.FindById(id);
            if (categoryId == null)
                return NotFound();

            return View(categoryId);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var categoryId = _categoryRepository.FindById(id);
            if (categoryId == null)
                return NotFound();
            else
                _categoryRepository.DeleteCategory(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
