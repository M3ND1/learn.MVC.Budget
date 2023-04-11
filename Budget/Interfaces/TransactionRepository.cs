using Budget.Data;
using Budget.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Budget.Interfaces
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BudgetContext _context;
        public TransactionRepository(BudgetContext context)
        {
            _context = context;
        }

        public void CreateTransaction(Transaction transaction)
        {
            var category = _context.Categories.Find(transaction.CategoryId);
            if(category == null)
            {
                throw new ArgumentException("Invalid categoryId");
            }
            transaction.Category = category;
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public void DeleteTransaction(Transaction transaction)
        {
            _context.Transactions.Remove(transaction);
            _context.SaveChanges();
        }

        public Transaction FindId(int id)
        {
            var transaction = _context.Transactions.FirstOrDefault(c => c.Id == id);
            return transaction;
        }
        public List<Transaction> IncludeAll()
        {
            var transactions = _context.Transactions.Include(t => t.Category).ToList();
            return transactions;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
