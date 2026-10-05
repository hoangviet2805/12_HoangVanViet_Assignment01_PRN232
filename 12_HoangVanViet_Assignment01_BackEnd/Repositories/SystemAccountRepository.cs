using System.Collections.Generic;
using _12_HoangVanViet_Assignment01_BackEnd.DAOs;
using _12_HoangVanViet_Assignment01_BackEnd.Models;

namespace _12_HoangVanViet_Assignment01_BackEnd.Repositories
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        public void AddAccount(SystemAccount account) => SystemAccountDAO.Instance.AddAccount(account);
        public void DeleteAccount(SystemAccount account) => SystemAccountDAO.Instance.DeleteAccount(account);
        public SystemAccount? GetAccountByEmail(string email) => SystemAccountDAO.Instance.GetAccountByEmail(email);
        public SystemAccount? GetAccountById(short id) => SystemAccountDAO.Instance.GetAccountById(id);
        public IEnumerable<SystemAccount> GetAccounts() => SystemAccountDAO.Instance.GetAccounts();
        public void UpdateAccount(SystemAccount account) => SystemAccountDAO.Instance.UpdateAccount(account);
    }
}
