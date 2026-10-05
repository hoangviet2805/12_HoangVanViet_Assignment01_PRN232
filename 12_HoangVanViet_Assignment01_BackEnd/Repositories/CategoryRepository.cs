using System.Collections.Generic;
using _12_HoangVanViet_Assignment01_BackEnd.DAOs;
using _12_HoangVanViet_Assignment01_BackEnd.Models;

namespace _12_HoangVanViet_Assignment01_BackEnd.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public void AddCategory(Category category) => CategoryDAO.Instance.AddCategory(category);
        public void DeleteCategory(Category category) => CategoryDAO.Instance.DeleteCategory(category);
        public IEnumerable<Category> GetCategories() => CategoryDAO.Instance.GetCategories();
        public Category? GetCategoryById(short id) => CategoryDAO.Instance.GetCategoryById(id);
        public void UpdateCategory(Category category) => CategoryDAO.Instance.UpdateCategory(category);
    }
}
