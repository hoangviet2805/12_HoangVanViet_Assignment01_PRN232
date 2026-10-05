using System.Collections.Generic;
using System.Linq;
using _12_HoangVanViet_Assignment01_BackEnd.Models;

namespace _12_HoangVanViet_Assignment01_BackEnd.DAOs
{
    public class SystemAccountDAO
    {
        private static SystemAccountDAO? _instance;
        private static readonly object _instanceLock = new object();

        private SystemAccountDAO() { }

        public static SystemAccountDAO Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new SystemAccountDAO();
                    }
                    return _instance;
                }
            }
        }

        public IEnumerable<SystemAccount> GetAccounts()
        {
            using var context = new FUNewsManagementContext();
            return context.SystemAccounts.ToList();
        }

        public SystemAccount? GetAccountById(short id)
        {
            using var context = new FUNewsManagementContext();
            return context.SystemAccounts.FirstOrDefault(c => c.AccountID == id);
        }

        public SystemAccount? GetAccountByEmail(string email)
        {
            using var context = new FUNewsManagementContext();
            return context.SystemAccounts.FirstOrDefault(c => c.AccountEmail == email);
        }

        public void AddAccount(SystemAccount account)
        {
            using var context = new FUNewsManagementContext();
            context.SystemAccounts.Add(account);
            context.SaveChanges();
        }

        public void UpdateAccount(SystemAccount account)
        {
            using var context = new FUNewsManagementContext();
            context.SystemAccounts.Update(account);
            context.SaveChanges();
        }

        public void DeleteAccount(SystemAccount account)
        {
            using var context = new FUNewsManagementContext();
            context.SystemAccounts.Remove(account);
            context.SaveChanges();
        }
    }
}
