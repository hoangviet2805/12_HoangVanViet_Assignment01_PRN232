using System.Collections.Generic;
using _12_HoangVanViet_Assignment01_BackEnd.Models;

namespace _12_HoangVanViet_Assignment01_BackEnd.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
        Category? GetCategoryById(short id);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
