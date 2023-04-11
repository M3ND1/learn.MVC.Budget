using Budget.Data;
using Budget.Interfaces;
using Budget.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Budget.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public IActionResult Index()
        {
            var transactions = _transactionRepository.IncludeAll();

            foreach (var transaction in transactions)
            {
              ViewBag.CategoryName = transaction.Category?.Name;
            }
            return View(transactions);
        }
        public IActionResult Create()
        {
            //var transaction = _context.Transactions.Include(t => t.Category).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("Name,Description,Sum,DateTime,CategoryId")] Transaction transaction)
        {
            //transaction.Id = 0;
            if (ModelState.IsValid)
            {
                _transactionRepository.CreateTransaction(transaction);
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }
        public IActionResult Edit(int id)
        {
            var data = _transactionRepository.FindId(id);
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

            var exisitngId = _transactionRepository.FindId(id);
            if (exisitngId == null)
                return View(newtransaction);

            exisitngId.Name = newtransaction.Name;
            _transactionRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //createt function findbyId
        public IActionResult Details(int id)
        {
            var data = _transactionRepository.FindId(id);
            if (data == null)
                return NotFound();

            return View(data);
        }
        public IActionResult Delete(int id)
        {
            var data = _transactionRepository.FindId(id);
            if (data == null)
                return NotFound();

            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _transactionRepository.FindId(id);
            if (data != null)
            {
                _transactionRepository.DeleteTransaction(data);

            }
            return RedirectToAction(nameof(Index));
        }
    }
}
