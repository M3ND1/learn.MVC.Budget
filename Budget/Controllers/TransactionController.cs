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

            foreach (var transaction in transactions)
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
        public IActionResult Create([Bind("Name,Description,Sum,DateTime,CategoryId")] Transaction transaction)
        {
            //transaction.Id = 0;
            var category = _context.Categories.Find(transaction.CategoryId);
            if (category == null)
            {
                return View();
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
            if (data == null)
            {
                return NotFound(); //eo internet
            }
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Name,Description,Sum,DateTime,CategoryId")] Transaction newtransaction)
        {
            if (!ModelState.IsValid)
                return View(newtransaction);

            var exisitngId = _context.Transactions.FirstOrDefault(c => c.Id == id);
            if (exisitngId == null)
                return View(newtransaction);

            exisitngId.Name = newtransaction.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //createt function findbyId
        public IActionResult Details(int id)
        {
            var data = _context.Transactions.FirstOrDefault(t => t.Id == id);
            if (data == null)
                return NotFound();

            return View(data);
        }
        public IActionResult Delete(int id)
        {
            var data = _context.Transactions.FirstOrDefault(t => t.Id == id);
            if (data == null)
                return NotFound();

            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _context.Transactions.FirstOrDefault(t => t.Id == id);
            if (data != null)
            {
                _context.Transactions.Remove(data);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
