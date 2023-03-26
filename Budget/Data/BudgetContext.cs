using Budget.Models;
using Microsoft.EntityFrameworkCore;
namespace Budget.Data
{
    public class BudgetContext : DbContext
    {
        public BudgetContext(DbContextOptions<BudgetContext> options) : base(options) { }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryId)
                .HasConstraintName("CategoryId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
