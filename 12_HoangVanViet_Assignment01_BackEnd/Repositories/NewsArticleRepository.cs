using System.Collections.Generic;
using _12_HoangVanViet_Assignment01_BackEnd.DAOs;
using _12_HoangVanViet_Assignment01_BackEnd.Models;

namespace _12_HoangVanViet_Assignment01_BackEnd.Repositories
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        public void AddNewsArticle(NewsArticle article) => NewsArticleDAO.Instance.AddNewsArticle(article);
        public void DeleteNewsArticle(NewsArticle article) => NewsArticleDAO.Instance.DeleteNewsArticle(article);
        public NewsArticle? GetNewsArticleById(string id) => NewsArticleDAO.Instance.GetNewsArticleById(id);
        public IEnumerable<NewsArticle> GetNewsArticles() => NewsArticleDAO.Instance.GetNewsArticles();
        public void UpdateNewsArticle(NewsArticle article) => NewsArticleDAO.Instance.UpdateNewsArticle(article);
    }
}
