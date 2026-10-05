using _12_HoangVanViet_Assignment01_BackEnd.Models;
using _12_HoangVanViet_Assignment01_BackEnd.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Linq;

namespace _12_HoangVanViet_Assignment01_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ISystemAccountRepository _repository;

        public AccountsController(ISystemAccountRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAccounts());
        }

        [HttpPost]
        public IActionResult Post([FromBody] SystemAccount account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repository.AddAccount(account);
            return CreatedAtAction(nameof(GetAccount), new { id = account.AccountID }, account);
        }

        [HttpGet("/odata/Accounts")]
        [EnableQuery]
        public IActionResult GetOData()
        {
            return Ok(_repository.GetAccounts().AsQueryable());
        }

        [HttpGet("/odata/Accounts/$count")]
        public IActionResult GetODataCount()
        {
            return Ok(_repository.GetAccounts().Count());
        }

        [HttpGet("{id}")]
        public IActionResult GetAccount(short id)
        {
            var account = _repository.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPut("{id}")]
        public IActionResult Put(short id, [FromBody] SystemAccount account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != account.AccountID)
            {
                return BadRequest();
            }

            var existing = _repository.GetAccountById(id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.AccountName = account.AccountName;
            existing.AccountEmail = account.AccountEmail;
            existing.AccountRole = account.AccountRole;
            existing.AccountPassword = account.AccountPassword;

            _repository.UpdateAccount(existing);
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(short id)
        {
            var account = _repository.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }
            _repository.DeleteAccount(account);
            return NoContent();
        }

        [HttpPut("profile/{id}")]
        public IActionResult UpdateProfile(short id, [FromBody] SystemAccount account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != account.AccountID)
            {
                return BadRequest();
            }

            var existing = _repository.GetAccountById(id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.AccountName = account.AccountName;
            existing.AccountEmail = account.AccountEmail;
            existing.AccountPassword = account.AccountPassword;

            _repository.UpdateAccount(existing);
            return Ok(existing);
        }
    }
}
