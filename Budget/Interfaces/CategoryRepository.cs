using Budget.Data;
using Budget.Models;

namespace Budget.Interfaces
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BudgetContext _context;
        public CategoryRepository(BudgetContext context)
        {
            _context = context;
        }
        public List<Category> GetAllCategories()
        {
            var data = _context.Categories.ToList();
            return data;
        }
        public void AddCategory(Category category)
        { 
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public Category FindById(int id)
        {
            var data = _context.Categories.FirstOrDefault(c => c.Id == id);
            return data;
        }

        public void EditCategory(int id, Category newcategory)
        {
            var exsitingCat = FindById(id);
            if (exsitingCat != null)
            {
                exsitingCat.Name = newcategory.Name;
                _context.SaveChanges();
            }
            else throw new ArgumentException($"Category with id: {id}, not found.");
        }

        public Category DeleteCategory(int id)
        {
            var categoryId = FindById(id);
            _context.Categories.Remove(categoryId);
            _context.SaveChanges();

            return categoryId;
        }
        
    }
}
