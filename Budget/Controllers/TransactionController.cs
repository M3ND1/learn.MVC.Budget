using Budget.Data;
using Budget.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Budget.Controllers
{
    public class TransactionController : Controller
    {
        private readonly BudgetContext _context;
        public TransactionController(BudgetContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var transactions = _context.Transactions.Include(t => t.Category).ToList(); 

            foreach(var transaction in transactions)
            {
                var data = _context.Categories.Find(transaction.CategoryId); //INNER JOIN
                ViewBag.CategoryName = data?.Name;
            }
            return View(transactions);
        }
        public IActionResult Create()
        {
            var transaction = _context.Transactions.Include(t => t.Category).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("Name,Description,Sum,DateTime,CategoryId")]Transaction transaction)
        {
            //transaction.Id = 0;
            var category = _context.Categories.Find(transaction.CategoryId);
            if (category == null)
            {
                return NotFound();
            }
            transaction.Category = category;
            if (ModelState.IsValid)
            {
                _context.Transactions.Add(transaction);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }
        public IActionResult Edit(int id)
        {
            var data = _context.Transactions.FirstOrDefault(c => c.Id == id);
            if(data == null)
            {
                return NotFound(); //eo internet
            }
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Name,Description,Sum,DateTime,CategoryId")] Transaction transaction)
        {
            if(!ModelState.IsValid)
                return View(transaction);

            _context.Update(transaction);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //createt function findbyId
    }
}
