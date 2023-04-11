using Budget.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Budget.Interfaces
{
    public interface ITransactionRepository
    {
        List<Transaction> IncludeAll();
        Transaction FindId(int id);
        void CreateTransaction(Transaction transaction);
        void SaveChanges();
        void DeleteTransaction(Transaction transaction);
    }
}
