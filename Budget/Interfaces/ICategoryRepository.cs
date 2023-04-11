using Budget.Models;

namespace Budget.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        void AddCategory(Category category);
        Category FindById(int id);
        void EditCategory(int id, Category newcategory);
        Category DeleteCategory(int id);
    }
}
