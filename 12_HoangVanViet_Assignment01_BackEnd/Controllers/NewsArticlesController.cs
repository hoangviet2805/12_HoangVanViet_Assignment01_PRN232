using _12_HoangVanViet_Assignment01_BackEnd.Models;
using _12_HoangVanViet_Assignment01_BackEnd.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace _12_HoangVanViet_Assignment01_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsArticlesController : ControllerBase
    {
        private readonly INewsArticleRepository _repository;

        public NewsArticlesController(INewsArticleRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetNewsArticles());
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewsArticle article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repository.AddNewsArticle(article);
            return CreatedAtAction(nameof(GetNewsArticle), new { id = article.NewsArticleID }, article);
        }

        [HttpGet("{id}")]
        public IActionResult GetNewsArticle(string id)
        {
            var article = _repository.GetNewsArticleById(id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] NewsArticle article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != article.NewsArticleID)
            {
                return BadRequest();
            }

            var existing = _repository.GetNewsArticleById(id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.NewsTitle = article.NewsTitle;
            existing.Headline = article.Headline;
            existing.NewsContent = article.NewsContent;
            existing.NewsSource = article.NewsSource;
            existing.CategoryID = article.CategoryID;
            existing.NewsStatus = article.NewsStatus;
            existing.UpdatedByID = article.UpdatedByID;
            existing.ModifiedDate = System.DateTime.UtcNow;

            _repository.UpdateNewsArticle(existing);
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var article = _repository.GetNewsArticleById(id);
            if (article == null)
            {
                return NotFound();
            }
            _repository.DeleteNewsArticle(article);
            return NoContent();
        }

        [HttpGet("author/{authorId}")]
        public IActionResult GetByAuthor(short authorId)
        {
            var articles = _repository.GetNewsArticles().Where(a => a.CreatedByID == authorId).ToList();
            return Ok(articles);
        }
    }
}
