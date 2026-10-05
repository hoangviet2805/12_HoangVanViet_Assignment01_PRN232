using System.Collections.Generic;
using _12_HoangVanViet_Assignment01_BackEnd.Models;

namespace _12_HoangVanViet_Assignment01_BackEnd.Repositories
{
    public interface INewsArticleRepository
    {
        IEnumerable<NewsArticle> GetNewsArticles();
        NewsArticle? GetNewsArticleById(string id);
        void AddNewsArticle(NewsArticle article);
        void UpdateNewsArticle(NewsArticle article);
        void DeleteNewsArticle(NewsArticle article);
        IEnumerable<NewsArticle> GetStatistics(System.DateTime startDate, System.DateTime endDate);
    }
}
